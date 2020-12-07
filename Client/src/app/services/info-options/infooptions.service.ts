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
  public clientsJoined!: string;
  public coordinateSend!: string;
  public createClient!: string;
  public hitPointsInvoke!: string;
  public countInvoke!: string;
  public clientsJoinedAsync!: string;
  public coordinateSendAsync!: string;
  public turnFirstPlayer!: number;
  public turnSecondPlayer!: number;
  public hitPoint!: number;
  public hitPointMax!: number;
  public countCreateShip!: number;
  public countEmpty!: number;
  public coordinateCountMax!: number;
  public shipCreateMax!: number;
  public coordinate!: number;
  public msgWin!: string;
  public msgLose!: string;
  public coordinateCreate!: string;
  public getCoordinateCreate!: string;
  public createName!: string;

  constructor() {
    this.msgWin = 'You Win';
    this.msgLose = 'You Lose';
    this.sizeBoard = 10;
    this.msgUserCount = 'user-count';
    this.shipCount = 5;
    this.playerSize = 2;
    this.msgName = 'name';
    this.msgPlayer = 'msgPlayer';
    this.msgConnectionPlayer = 'msgPlayers';
    this.clientsJoined = 'clientsJoined';
    this.coordinateSend = 'coordinateSend';
    this.createClient = 'CreateClient';
    this.hitPointsInvoke = 'HitPointsInvoke';
    this.countInvoke = 'CountInvoke';
    this.clientsJoinedAsync = 'ClientsJoinedAsync';
    this.coordinateSendAsync = 'CoordinateSendAsync';
    this.turnFirstPlayer = 0;
    this.turnSecondPlayer = 1;
    this.hitPoint = 1;
    this.hitPointMax = 20;
    this.countCreateShip = 1;
    this.countEmpty = 0;
    this.coordinateCountMax = 20;
    this.shipCreateMax = 10;
    this.coordinate = 1;
    this.coordinateCreate = 'CoordinateCreate';
    this.getCoordinateCreate = 'coordinateCreate';
    this.createName = 'NameCreate';
  }
}
