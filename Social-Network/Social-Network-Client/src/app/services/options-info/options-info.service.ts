import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class OptionsInfoService {
  public host!: string;
  public getUserData!: string;
  public getAdminData!:string;
  public loginPost!: string;
  public authToken!: string;
  public loginPath!: string;
  public returnUrl!: string;
  public registerPost!: string;
  public header!: Headers;
  public imagePost!: string;
  public imageGet!: string;
  public blobConfig!: string;
  public userAllGet!: string;
  public editPath!: string;
  public userGet!: string;
  public editPost!: string;
  public eventAddToFriend!: string;
  public getEventAddToFriend!: string;
  public removeEvent!: string;
  public hubToConnect!: string;
  public addToFriendPost!: string;
  public getUsersInFriends!: string;
  public messagePost!: string;
  public messageAllGet!: string;
  public eventMessagePost!: string;
  public messagesPath!: string;
  public eventMessageRemove!: string;

  constructor() {
    // '/api/';
    this.host = 'https://localhost:44391/';
    this.getUserData = this.host + 'api/user/get-user-data';
    this.getAdminData = this.host + 'api/user/get-admin-data';
    this.loginPost = this.host + 'api/useraccount/login';
    this.authToken = 'authToken';
    this.loginPath = '/login';
    this.returnUrl = 'returnUrl';
    this.registerPost = this.host + 'api/useraccount/register';
    this.header = new Headers({'Content-Type': 'application/json'});
    this.imagePost = this.host + 'api/useraccount/image-upload';
    this.imageGet = this.host + 'api/useraccount/getfile';
    this.blobConfig = '?sv=2019-12-12&ss=bqtf&srt=sco&sp=rwdlacuptfx&se=2020-12-22T16:12:04Z&sig=bm62tPLhiEyYlyRQDR9KL8VqZxSnXIoSF4%2BKwyAp8DI%3D&_=1608626588679';
    this.userAllGet = this.host + 'api/friend/get-user-all';
    this.editPath = '/edit-user';
    this.userGet = this.host + 'api/useraccount/user-get';
    this.editPost = this.host + 'api/edit/user-edit';
    this.eventAddToFriend = this.host + 'api/notification/event-add-to-friend';
    this.getEventAddToFriend = this.host + 'api/notification/get-event-add-to-friend';
    this.removeEvent = this.host + 'api/notification/remove-event-add-to-friend';
    this.hubToConnect = this.host + 'hubToConnect';
    this.addToFriendPost = this.host + 'api/friend/user-add-to-friends';
    this.getUsersInFriends = this.host + 'api/friend/get-users-in-friends';
    this.messagePost = this.host + 'api/message/message-create';
    this.messageAllGet = this.host + 'api/message/message-all-get';
    this.eventMessagePost = this.host + 'api/notification/event-messages-create';
    this.messagesPath = '/messages';
    this.eventMessageRemove = this.host + 'api/notification/event-messages-remove';
  }
}
