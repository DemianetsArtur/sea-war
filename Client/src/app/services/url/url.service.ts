import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UrlService {
  public host!: string;
  public signalRService!: string;
  public createPlayer!: string;
  public gameBoard!: string;
  constructor() {
    this.host = 'https://localhost:44360';
    this.signalRService = this.host + '/toGetData';
    this.createPlayer = this.host + '/api/player/player-create';
    this.gameBoard = 'game-play';
   }
}
