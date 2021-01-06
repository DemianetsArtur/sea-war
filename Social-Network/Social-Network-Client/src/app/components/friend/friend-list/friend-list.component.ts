import { Component, OnInit } from '@angular/core';
import { UserAccount } from 'src/app/models/user-account/user-account';
import { ConnectService } from 'src/app/services/connect/connect.service';
import { OptionsInfoService } from 'src/app/services/options-info/options-info.service';
import { faTimes } from '@fortawesome/free-solid-svg-icons';
import { AlertService } from '../../../services/alert/alert.service';
import { NotificationFriendInfo } from '../../../models/notification/notification-add-to-friend/notification-friend-info';
import { tap, catchError } from 'rxjs/operators';
import { Friend } from '../../../models/friend/friend';
import { NotificationInfoService } from 'src/app/services/notification/notification-info/notification-info.service';

@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.css']
})
export class FriendListComponent implements OnInit {
  public userAccountSubscription!: any;
  public userAccountArraySubscription!: any;
  public usersInFriendsSubscription!: any;
  public notificationAddToFriendSubscription!: any;
  public userData = new UserAccount();
  public userArray!: UserAccount[];
  public allUser!: any[];
  public faTimes = faTimes;
  public blockAddToFriend = false;
  public usersInFriendsArray: Friend[] = [];
  public usersInFriends: Friend[] = [];
  public userFind = '';
  public notificationAddToFriend: NotificationFriendInfo[] = []; 
  public notificationArray!: NotificationFriendInfo[];
  public unblock = false;

  constructor(private connect: ConnectService, 
              private optionInfo: OptionsInfoService, 
              private alert: AlertService,
              private notificationInfoService: NotificationInfoService) {
    this.userAccountSubscription = this.connect.userAccountData$.subscribe(value => {
      this.userData = value;
    });
    this.userAccountArraySubscription = this.connect.userAllGet(this.userData.name);
    this.usersInFriendsSubscription = this.connect.usersInFriendsGet();
  }

  ngOnInit(): void {
    this.hubConnect(); 
  }

  

  public addFriend = (nameResponse: string, nameToResponse: string) => {
    this.setIsBlock(nameToResponse);
    const notificationInfo = new NotificationFriendInfo();
    notificationInfo.userNameResponse = nameResponse;
    notificationInfo.userNameToResponse = nameToResponse;
    this.connect.eventAddToFriend(notificationInfo).pipe(tap(data => {
        this.connect.startConnection();
        this.connect.handlerGetNotificationAddToFriend();
    }),catchError(async (err) => {
      
    })).subscribe();
  }

  private setIsBlock = (nameResponse: string) => {
    for(const user of this.userArray){
      if(user.name === nameResponse){
        user.isBlock = true;
      }
    }
  }

  public filterData = (valueFilter: string) => {
    debugger;
    if (valueFilter === ""){
      this.userArray = this.allUser;
    }
    else{
      const value = valueFilter.toLocaleLowerCase();
      this.userArray = this.allUser.filter(val => val.name.toLowerCase() === value);
      if (this.userArray.length === 0) {
          this.alert.userNickNameNotExist();
      }
    }
  }

  public removeFilterData = () => {
    this.userArray = this.allUser;
    this.userFind = '';
  }

  private getUsersInFriends = () => {
    this.usersInFriendsSubscription = this.connect.usersInFriends$.subscribe(value => {
    });
  }

  private getNotificationAddToFriend = (nameToResponse: string) => {
    const notificationInfo = new NotificationFriendInfo();
    notificationInfo.userNameToResponse = nameToResponse;
    notificationInfo.userNameResponse = nameToResponse;
    this.connect.getEventAddToFriend(notificationInfo);
    this.notificationAddToFriendSubscription = this.connect.notificationAddToFriends$.subscribe(value => {
      if (value !== undefined) {
        this.notificationArray = value;
        this.notificationAddToFriend = this.notificationArray.filter(name => name.userNameResponse === this.userData.name && name.nameResponse === this.notificationInfoService.eventAddToFriend);
        debugger;
        console.log("n:", this.notificationAddToFriend);
        if (this.notificationAddToFriend.length !== 0){
          this.setUserBlock();
        }
      }
    });
  }

  private hubConnect = () => {
    this.connect.startConnection();
    this.connect.handlerGetUsersInFriendship();
    this.connect.handlerGetNotificationAddToFriend();
    this.getNotificationAddToFriend(this.userData.name);
    this.getUsersInFriends();

    if (this.userData !== undefined){
      this.userAccountArraySubscription = this.connect.userAccountArray$.subscribe(value => {
        this.allUser = value;
        this.userArray = this.allUser;
        
      });
      this.usersInFriendsSubscription = this.connect.usersInFriends$.subscribe(value => {
        this.usersInFriendsArray = value;
        this.usersInFriends = this.usersInFriendsArray.filter(name => name.userNameResponse === this.userData.name);
        if (this.usersInFriendsArray.length !== 0){
          this.setUsersParameters();
        }
      });  
      this.notificationAddToFriendSubscription = this.connect.notificationAddToFriends$.subscribe(value => {
        if (value !== undefined) {
          this.notificationArray = value;
          this.notificationAddToFriend = this.notificationArray.filter(name => name.userNameResponse === this.userData.name 
                                                                       && name.nameResponse === this.notificationInfoService.eventAddToFriend);
          if (this.notificationAddToFriend.length !== 0){
            console.log('notif: ',this.notificationAddToFriend);
            this.setUserBlock();
          }
          else{
            for(const user of this.userArray){
              if(user.isBlock === true){
                debugger;
                user.isBlock = false;
              }
              debugger;
            }
          }
        }
      });   
    }
  }
  private setUsersParameters = () => {
    
    for(const users of this.userArray){
      for(const friends of this.usersInFriends){
        if(users.name === friends.userNameToResponse){
          users.isFriends = true;
        }
      }
    }
  }
  
  private setUserBlock = () => {
    debugger;
    for (const user of this.userArray){
      for (const event of this.notificationAddToFriend){
        if(user.name === event.userNameToResponse && event.nameResponse === this.notificationInfoService.eventAddToFriend){
          user.isBlock = true;
        }
        else{
          user.isBlock = false;
        }
      }
    }
  }
}
