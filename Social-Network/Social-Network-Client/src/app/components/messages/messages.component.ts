import { Component, OnInit } from '@angular/core';
import { UserAccount } from 'src/app/models/user-account/user-account';
import { ConnectService } from 'src/app/services/connect/connect.service';
import { MessageInfo } from '../../models/message/message-info';
import { MessageGet } from 'src/app/models/message/message-get';
import { NotificationMessages } from '../../models/notification-messages/notification-messages';
import { catchError, tap } from 'rxjs/operators';
import { AlertService } from '../../services/alert/alert.service';
import { Friend } from 'src/app/models/friend/friend';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  public userAccountSubscription!: any;
  public userAccountArraySubscription!: any;
  public userAccountCurrentSubscription!: any;
  public usersInFriendsSubscription!: any;
  public messageAllSubscription!: any;
  public userAccountCurrentData = new UserAccount();
  public userData = new UserAccount();
  public userArray!: UserAccount[];
  public allUser!: any[];
  public messageInfo = new MessageInfo();
  public userText = '';
  public messageAllArray!: MessageGet[];
  public messagesEmpty = true;
  public usersInFriendsArray: Friend[] = [];
  public usersInFriends: Friend[] = [];
  public isUserInFriends = false;

  constructor(private connect: ConnectService, 
              private alertService: AlertService) {
    this.userAccountSubscription = this.connect.userAccountData$.subscribe(value => {
      this.userData = value;
    });
    this.userAccountArraySubscription = this.connect.userAllGet(this.userData.name);
    this.userAccountArraySubscription = this.connect.userAccountArray$.subscribe(value => {
      this.allUser = value;
      this.userArray = this.allUser; 
    });
    this.userAccountCurrentSubscription = this.connect.userGet(this.userData.name);
    if (this.userData !== undefined){
      this.userAccountCurrentSubscription = this.connect.userAccountCurrentValue$.subscribe(value => {
        this.userAccountCurrentData = value;
      });
    }
  }

  ngOnInit(): void {
  }

  public selectUserForMessage = (user: UserAccount) => {
    this.messageInfo.userNameResponse = this.userAccountCurrentData.name;
    this.messageInfo.userNameToResponse = user.name;
    this.messageInfo.userImageResponse = this.userAccountCurrentData.imagePath;
    this.messageInfo.userImageToResponse = user.imagePath;
    this.messageInfo.isInFriends = false;
    this.userText = '';
    this.hubConnect();
  }

  public sendMessage = () => {
    this.messageInfo.text = this.userText;
    debugger;
    this.connect.messagePost(this.messageInfo).subscribe(_ => {
      this.userText = '';
      this.messageAllGet(this.messageInfo);
    });
    this.eventMessagesCreate();
  }

  private eventMessagesCreate = () => {
    const notificationInfo = new NotificationMessages();
    notificationInfo.userNameResponse = this.userData.name;
    notificationInfo.userNameToResponse = this.messageInfo.userNameToResponse;
    notificationInfo.textMessage = this.messageInfo.text;
    this.connect.eventMessagePost(notificationInfo).pipe(tap(data => {
      this.connect.startConnection();
      this.connect.handlerGetNotificationAddToFriend();
    }),catchError(async (err) => {
    })).subscribe();
  }

  private messageAllGet = (messageInfo: MessageInfo) => {
    if (messageInfo !== undefined){
      this.connect.messageAllGet(messageInfo).subscribe();
      this.messageAllSubscription = this.connect.messageAll$.subscribe(value => {
      if (value !== undefined) {
        this.messageAllArray = value;
        if(this.messageAllArray.length === 0){
          this.messagesEmpty = true;
        }
        else{
          this.messagesEmpty = false;
        }
      }
    });
    }
  }

  private hubConnect = () =>{
    this.connect.startConnection();
    this.connect.handlerGetMessageAll();
    this.messageAllGet(this.messageInfo);
    this.connect.handlerGetUsersInFriendship();

    if (this.userData !== undefined){  
      this.usersInFriendsSubscription = this.connect.usersInFriends$.subscribe(value => {
        this.usersInFriendsArray = value;
        this.usersInFriends = this.usersInFriendsArray.filter(name => name.userNameResponse === this.userData.name);
        console.log('all: ', this.usersInFriendsArray);
        console.log('in fr: ', this.usersInFriendsArray);
        if (this.usersInFriendsArray.length !== 0){
          this.userInFriends();
        }
      });    
    }
  }

  private userInFriends = () => {
      for(const friends of this.usersInFriends){
        if(this.messageInfo.userNameToResponse === friends.userNameToResponse){
          
          this.isUserInFriends = true;
          this.messageInfo.isInFriends = true;
          console.log('messaage: ', this.messageInfo);
        }
      }
    }
}

