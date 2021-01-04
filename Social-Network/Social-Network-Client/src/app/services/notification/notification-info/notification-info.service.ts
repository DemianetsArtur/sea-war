import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NotificationInfoService {
  public eventMessages!: string;
  constructor() {
    this.eventMessages = 'Event Messages';
   }
}
