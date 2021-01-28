import { catchError, delay, tap } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserAccount } from 'src/app/models/user-account/user-account';
import { ConnectService } from 'src/app/services/connect/connect.service';
import { NotificationFriendInfo } from 'src/app/models/notification/notification-add-to-friend/notification-friend-info';
import { NotificationInfoService } from 'src/app/services/notification/notification-info/notification-info.service';
import { Friend } from 'src/app/models/friend/friend';

@Component({
  selector: 'app-users-profile',
  templateUrl: './users-profile.component.html',
  styleUrls: ['./users-profile.component.css']
})
export class UsersProfileComponent implements OnInit {
  public userAccountSubscription!: any;
  public userCurrentData = new UserAccount();
  public userData = new UserAccount();
  private userName!: string;
  public notificationAddToFriendSubscription!: any;
  public notificationAddToFriend: NotificationFriendInfo[] = []; 
  public notificationArray!: NotificationFriendInfo[];
  public usersInFriendsSubscription!: any;
  public usersInFriendsArray: Friend[] = [];
  public usersInFriends: Friend[] = [];

  constructor(private connect: ConnectService, 
              private route: ActivatedRoute, 
              private notificationInfoService: NotificationInfoService) { 
    this.route.queryParams.subscribe(opt => {
      this.userName = opt['nickname'];
    });
    this.userAccountSubscription = this.connect.userAccountData$.subscribe(value => {
      this.userCurrentData = value;
    });
    this.userAccountSubscription = this.connect.usersGet(this.userName);
    this.connect.userGetData$
                .pipe()
                .subscribe(opt => {
      this.userData = opt;
    });
    this.hubConnect();
  }

  ngOnInit(): void {
    this.hubConnect();
  }

  public addFriend = (nameResponse: string, nameToResponse: string) => {
    debugger;
    this.userData.isBlock = true;
    const notificationInfo = new NotificationFriendInfo();
    notificationInfo.userNameResponse = nameResponse;
    notificationInfo.userNameToResponse = nameToResponse;
    this.connect.eventAddToFriend(notificationInfo).pipe(tap(data => {
        this.hubConnect();
    }),catchError(async (err) => {
      
    })).subscribe();
  }

  private getNotificationAddToFriend = (nameToResponse: string) => {
    const notificationInfo = new NotificationFriendInfo();
    notificationInfo.userNameToResponse = nameToResponse;
    notificationInfo.userNameResponse = nameToResponse;
    this.connect.getEventAddToFriend(notificationInfo);
    this.notificationAddToFriendSubscription = this.connect.notificationAddToFriends$.subscribe(value => {
      if (value !== undefined) {
        this.notificationArray = value;
        this.notificationAddToFriend = this.notificationArray.filter(name => name.userNameResponse === this.userCurrentData.name && name.nameResponse === this.notificationInfoService.eventAddToFriend);
        if (this.notificationAddToFriend.length !== 0){
          this.setUserBlock();
        }
      }
    });
  }

  private setUserBlock = () => {
      const eventAddToFriends = this.notificationAddToFriend.filter(opt => opt.userNameResponse === this.userCurrentData.name && opt.userNameToResponse === this.userData.name 
                                                                 && opt.nameResponse === this.notificationInfoService.eventAddToFriend);
      if(eventAddToFriends.length !== 0){
        this.userData.isBlock = true;
      }
      else{
        this.userData.isBlock = false;
      }
  }

  private hubConnect = () => {
    this.connect.startConnection();
    this.connect.handlerGetUsersInFriendship();
    this.connect.handlerGetNotificationAddToFriend();
    this.getNotificationAddToFriend(this.userCurrentData.name);

    if (this.userData !== undefined){  
      this.usersInFriendsSubscription = this.connect.usersInFriends$.subscribe(value => {
        this.usersInFriendsArray = value;
        this.usersInFriends = this.usersInFriendsArray.filter(name => name.userNameResponse === this.userCurrentData.name && name.userNameToResponse === this.userData.name );
        if (this.usersInFriendsArray.length !== 0){
          this.setUsersParameters();
        }
      });  
      this.notificationAddToFriendSubscription = this.connect.notificationAddToFriends$.subscribe(value => {
        if (value !== undefined) {
          this.notificationArray = value;
          this.notificationAddToFriend = this.notificationArray.filter(name => name.userNameResponse === this.userCurrentData.name 
                                                                    && name.userNameToResponse === this.userData.name 
                                                                    && name.nameResponse === this.notificationInfoService.eventAddToFriend);
              if (this.notificationAddToFriend.length !== 0){
                this.setUserBlock();
              }
              else{
                if(this.userData.isBlock === true){
                  this.userData.isBlock = false;
                }
              }
        }
      });   
    }
  }

  private setUsersParameters = () => {
      for(const friends of this.usersInFriends){
        if(this.userData.name === friends.userNameToResponse){
          this.userData.isFriends = true;
        }
      }
  }
}
