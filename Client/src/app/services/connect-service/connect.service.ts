import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { InfoOptionsService } from '../info-options/infooptions.service';
import { UrlService } from '../url/url.service';
import { BehaviorSubject, from } from 'rxjs';
import { Player } from '../../models/player/player';
import { ChatMessage } from '../../models/chat-message/chat-message';
import { Coordinate } from '../../models/coordinate/coordinate';
import { Router } from '@angular/router';
import { AlertHandlerService } from '../alert-handler/handler-alert.service';

@Injectable({
  providedIn: 'root'
})
export class ConnectService {
  private hubConnection!: signalR.HubConnection;
  public playerData = new BehaviorSubject<Player[]>([]);
  public PlayerData$ = this.playerData.asObservable();
  public nameClient: BehaviorSubject<string> = new BehaviorSubject<string>('');
  public messages: ChatMessage[] = [];
  public coordinates: Coordinate[] = [];
  public names: Player[] = [];
  public isPlayerRemove: Player[] = [];

  constructor(private info: InfoOptionsService,
              private url: UrlService,
              private router: Router,
              private alert: AlertHandlerService) { }

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                                    .withUrl(this.url.signalRService)
                                    .build();
    this.hubConnection.start()
                      .then(() => console.log('signal r connection start'))
                      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public addTransferDataListener = () => {
    this.hubConnection?.on(this.info.msgConnectionPlayer, (data: Player[]) => {
      this.playerData.next(data);
    });
  }

  public addCoordinatePlayerListener = () => {
    this.hubConnection.on(this.info.getCoordinateCreate, (data) => {
      this.playerData.next(data);
    });
  }

  public addPlayerRemoveListener = () => {
    this.hubConnection.on(this.info.removePlayer, (data) => {
      this.isPlayerRemove = data;
      if (data === null){
        this.removePlayerHandler();
      }
    });
  }

  public addTransferChatListener = () => {
    this.hubConnection.on(this.info.clientsJoined, (data: ChatMessage) => {
      this.messages.push(data);
    });
  }

  public addTransferNameListener = () => {
    this.hubConnection.on(this.info.createName, (data: Player) => {
      this.names.push(data);
      if (data === null){
        this.transferNameHandler();
      }
    });
  }

  public addTransferCoordinateListener = () => {
    this.hubConnection.on(this.info.coordinateSend, (data: Coordinate) => {
      this.coordinates.push(data);
    });
  }

  public createPlayerService(playerDetail: Player): void{
    this.nameClient.next(playerDetail.Name);
    this.hubConnection.invoke(this.info.createClient, playerDetail);
  }

  public addHitPointInvoke(playerDetail: Player): void{
    this.hubConnection.invoke(this.info.hitPointsInvoke, playerDetail);
  }

  public sendCountToHub = (playerDetail: Player) => {
    const promise = this.hubConnection.invoke(this.info.countInvoke, playerDetail);
    return from(promise);
  }

  public sendPlayersToHub = (playersDetails: Player[]) => {
    const promise = this.hubConnection.invoke(this.info.removePlayer, playersDetails);
    return from(promise);
  }

  public sendNameToHub = (playerDetail: Player) => {
    const promise = this.hubConnection.invoke(this.info.createName, playerDetail);
    return from(promise);
  }

  public sendMessageToHub = (message: ChatMessage) => {
    const promise = this.hubConnection.invoke(this.info.clientsJoinedAsync, message);
    return from(promise);
  }

  public sendCoordinatesPlayer = (entity: Player) => {
    const promise = this.hubConnection.invoke(this.info.coordinateCreate, entity);
    return from(promise);
  }

  public sendCoordinate = (coordinate: Coordinate) => {
    const promise = this.hubConnection.invoke(this.info.coordinateSendAsync, coordinate);
    return from(promise);
  }

  private removePlayerHandler = () => {
    const emptyPlayer: Player[] = [];
    this.playerData.next(emptyPlayer);
    this.router.navigate([this.info.boardUrl]);
  }

  private transferNameHandler = () => {
    this.alert.nameRepeat();
    this.router.navigate([this.info.boardUrl]);
  }
}
