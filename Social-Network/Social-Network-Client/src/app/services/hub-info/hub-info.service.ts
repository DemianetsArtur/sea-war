import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HubInfoService {
  public eventAddToFriendHub!: string;
  public usersInFriendship!: string;
  public messageAll!: string;
  public userAll!: string;
  public videoPath!: string;
  constructor() {
    this.eventAddToFriendHub = 'EventAddToFriendHub';
    this.usersInFriendship = 'UsersInFriendshipHub';
    this.messageAll = 'GetMessageAllHub';
    this.userAll = 'userAllHub';
    this.videoPath = 'https://socialnetworkname.blob.core.windows.net/content-container/video2.mp4';
   }
}
