import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HubInfoService {
  public eventAddToFriendHub!: string;
  public usersInFriendship!: string;
  constructor() {
    this.eventAddToFriendHub = 'EventAddToFriendHub';
    this.usersInFriendship = 'UsersInFriendshipHub';
   }
}
