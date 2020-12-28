import { Component, OnInit } from '@angular/core';
import { UserAccount } from 'src/app/models/user-account/user-account';
import { ConnectService } from 'src/app/services/connect/connect.service';
import { NotificationFriendInfo } from '../../models/notification/notification-add-to-friend/notification-friend-info';
import { Friend } from '../../models/friend/friend';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})

export class NotificationComponent implements OnInit {
  public userAccountSubscription!: any;
  public notificationAddToFriendSubscription!: any;
  public userData = new UserAccount();
  public notificationAddToFriend: NotificationFriendInfo[] = []; 
  public notificationArray!: NotificationFriendInfo[]; 
  constructor(private connect: ConnectService) {
    this.userAccountSubscription = this.connect.userAccountData$.subscribe(value => {
      this.userData = value;
    });   
  }

  ngOnInit(): void {
    this.hubConnect();
  }

  
  private getNotificationAddToFriend = (nameToResponse: string) => {
    const notificationInfo = new NotificationFriendInfo();
    notificationInfo.userNameToResponse = nameToResponse;
    notificationInfo.userNameResponse = nameToResponse;
    this.connect.getEventAddToFriend(notificationInfo);
    this.notificationAddToFriendSubscription = this.connect.notificationAddToFriends$.subscribe(value => {
      if (value !== undefined) {
        this.notificationArray = value;
        this.notificationAddToFriend = this.notificationArray.filter(name => name.userNameToResponse === this.userData.name);
      }
    });
  }

  public acceptEvent = (notificationInfo: NotificationFriendInfo) => {
    const friendInfo = new Friend();
    friendInfo.userNameResponse = notificationInfo.userNameResponse;
    friendInfo.userNameToResponse = notificationInfo.userNameToResponse;
    friendInfo.nameResponse = notificationInfo.nameResponse;
    this.connect.addToFriendPost(friendInfo).subscribe(res => {
      this.hubConnect();
    });
  }

  public cancelEvent = (notificationInfo: NotificationFriendInfo) => {
    this.connect.removeEvent(notificationInfo).subscribe(res => {
      this.hubConnect(); 
    });   
  }

  private hubConnect = () =>{
    this.connect.startConnection();
    this.connect.handlerGetNotificationAddToFriend();
    this.getNotificationAddToFriend(this.userData.name);
  }

}
