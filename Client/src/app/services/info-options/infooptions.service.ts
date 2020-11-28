import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class InfoOptionsService {
  public sizeBoard!: number;
  public msgUserCount!: string;
  public shipCount!: number;
  public playerSize!: number;
  public msgName!: string;
  public msgPlayer!: string;
  public msgConnectionPlayer!: string;

  constructor() {
    this.sizeBoard = 10;
    this.msgUserCount = 'user-count';
    this.shipCount = 5;
    this.playerSize = 2;
    this.msgName = 'name';
    this.msgPlayer = 'msgPlayer';
    this.msgConnectionPlayer = 'msgPlayers';
   }
}
