import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HubInfoService {
  public eventAddToFriendHub!: string;
  public usersInFriendship!: string;
  public messageAll!: string;
  public userAll!: string;
  constructor() {
    this.eventAddToFriendHub = 'EventAddToFriendHub';
    this.usersInFriendship = 'UsersInFriendshipHub';
    this.messageAll = 'GetMessageAllHub';
    this.userAll = 'userAllHub';
   }
}
