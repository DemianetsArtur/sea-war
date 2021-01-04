import { Component, OnInit } from '@angular/core';
import { faUser, faPencilAlt, faComment, faUsers, faBell, faHome } from '@fortawesome/free-solid-svg-icons';
import { NotificationFriendInfo } from 'src/app/models/notification/notification-add-to-friend/notification-friend-info';
import { UserAccount } from 'src/app/models/user-account/user-account';
import { ConnectService } from 'src/app/services/connect/connect.service';

@Component({
  selector: 'app-menu-profile',
  templateUrl: './menu-profile.component.html',
  styleUrls: ['./menu-profile.component.css']
})
export class MenuProfileComponent implements OnInit {
  public faUser = faUser;
  public faPencilAlt = faPencilAlt;
  public faComment = faComment;
  public faUsers = faUsers;
  public faBell = faBell;
  public faHome = faHome;
  public notificationSubscription!: any;
  public userAccountSubscription!: any;
  public notificationInfo: NotificationFriendInfo[] = []; 
  public userData = new UserAccount();
  public notificationArray!: NotificationFriendInfo[];
  constructor(private connect: ConnectService) {
    this.userAccountSubscription = this.connect.userAccountData$.subscribe(value => {
      this.userData = value;
    });
  }

  ngOnInit(): void {
    this.hubConnect();
  }

  private getNotificationInfo = (nameToResponse: string) => {
    const notificationInfo = new NotificationFriendInfo();
    notificationInfo.userNameToResponse = nameToResponse;
    notificationInfo.userNameResponse = nameToResponse;
    this.connect.getEventAddToFriend(notificationInfo);
    this.notificationSubscription = this.connect.notificationAddToFriends$.subscribe(value => {
      if (value !== undefined) {
        this.notificationArray = value;
        this.notificationInfo = this.notificationArray.filter(name => name.userNameToResponse === this.userData.name);
      }
    });
  }

  private hubConnect = () =>{
    this.connect.startConnection();
    this.connect.handlerGetNotificationAddToFriend();
    this.getNotificationInfo(this.userData.name);
  }

}
