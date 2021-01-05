import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NotificationInfoService {
  public eventMessages!: string;
  public eventAddToFriend!: string;
  constructor() {
    this.eventMessages = 'Event Messages';
    this.eventAddToFriend = 'Event Add To Friend';
   }
}
