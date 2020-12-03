import {  Component, OnInit,  } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AlertHandlerService } from 'src/app/services/alert-handler/handler-alert.service';
import { InfoOptionsService } from 'src/app/services/info-options/infooptions.service';
import { UrlService } from 'src/app/services/url/url.service';
import { ConnectService } from '../../services/connect-service/connect.service';
import { Player } from '../../models/player/player';
import { Coordinate } from '../../models/coordinate/coordinate';
import { ChatMessage } from '../../models/chat-message/chat-message';
import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: 'app-game-play',
  templateUrl: './game-play.component.html',
  styleUrls: ['./game-play.component.css']
})
export class GamePlayComponent implements OnInit  {
  public name: any;
  public player: any[] = {} as any[];
  public isPlayGame!: boolean;
  public isWin!: boolean;
  public isLose!: boolean;
  private playerCoordinates: Array<Coordinate> = [];
  public message = '';
  public messages: ChatMessage[] = [];
  public coordinates: Coordinate[] = [];
  public turnPlayer!: boolean;
  public turn = 0;
  public isOpponentShoot = false;
  private coordinateList: Array<Coordinate> = [];
  private coordinateMiss: Array<Coordinate> = [];
  constructor(private info: InfoOptionsService,
              private url: UrlService,
              private alertService: AlertHandlerService,
              private connect: ConnectService,
              private route: ActivatedRoute,
              private spinner: NgxSpinnerService) {

  }

  public sizeCol = Array(this.info.sizeBoard);

  ngOnInit(): void {
    const nameUrl = this.route.snapshot.paramMap.get('name');
    this.name = nameUrl;
    this.connect.startConnection();
    this.connect.addTransferDataListener();
    this.connect.addTransferChatListener();
    this.connect.addTransferCoordinateListener();
    this.connect.PlayerData$.subscribe(opt => {
      this.player = opt;
      this.isPlayGame = (this.player.length === this.info.playerSize) ? true : false;
      if (this.player.length === this.info.playerSize){
        this.isWin = this.isWinHandler();
        this.isLose = this.isLoseHandler();
      }
    });
    this.messages = this.connect.messages;
    this.coordinates = this.connect.coordinates;
    this.spinner.show();
  }

  private coordinateSend = (firstCoordinate: number, secondCoordinate: number) => {
    const coordinate = new Coordinate();
    coordinate.name = this.name;
    coordinate.firstCoordinate = firstCoordinate;
    coordinate.secondCoordinate = secondCoordinate;
    this.connect.sendCoordinate(coordinate).subscribe();
  }


  private setTurnPlayer = (name: string) => {
      const player = this.player[0];
      const playerRes = this.player.find(opt => opt.name === name);
      const opponentRes = this.player.find(opt => opt.name !== name);

      this.handlerCountPlayer();

      if (playerRes.name === player.name && playerRes.count === this.info.turnFirstPlayer){
        this.turn = this.info.turnSecondPlayer;
        this.handlerCountPlayer();
        this.handlerCountChange(opponentRes.name);
        return true;
      }
      else if (playerRes.name !== player.name && playerRes.count !== this.info.turnFirstPlayer){
        this.turn = this.info.turnFirstPlayer;
        this.handlerCountPlayer();
        this.handlerCountChange(opponentRes.name);
        return true;
      }
      else{
        return false;
      }
  }

  private handlerCountPlayer = () => {
    const playerDetails = new Player();
    playerDetails.Name = this.name;
    playerDetails.Count = this.turn;
    this.connect.sendCountToHub(playerDetails);
  }

  private handlerCountChange = (name: string) => {
    const playerDetails = new Player();
    playerDetails.Name = name;
    playerDetails.Count = this.turn;
    this.connect.sendCountToHub(playerDetails);
  }

  public setMainStyle(firstCoordinate: number, secondCoordinate: number): boolean {
    const player = this.player.find(opt => opt.name === this.name);
    const playerCoordinate = player.coordinates.find((opt: { firstCoordinate: number; secondCoordinate: number; }) =>
                                                            opt.firstCoordinate === firstCoordinate
                                                         && opt.secondCoordinate === secondCoordinate);
    if (playerCoordinate !== undefined){
      return true;
    }
    return false;
  }

  private setShootCoordinate = (firstCoordinate: number, secondCoordinate: number) => {
    const coordinate = new Coordinate();
    coordinate.firstCoordinate = firstCoordinate;
    coordinate.secondCoordinate = secondCoordinate;
    coordinate.name = this.name;
    this.coordinateList.push(coordinate);
  }

  public setMissStyle = (firstCoordinate: number, secondCoordinate: number) => {
    const coordinate = this.coordinateMiss.find(opt => opt.name === this.name
                                             && opt.firstCoordinate === firstCoordinate
                                             && opt.secondCoordinate === secondCoordinate);
    if (coordinate !== undefined){
      return true;
    }
    else{
      return false;
    }

  }

  public setOpponentStyle = (firstCoordinate: number, secondCoordinate: number) => {
    const playerCoordinate = this.coordinateList.find(opt => opt.name === this.name
                                                   && opt.firstCoordinate === firstCoordinate
                                                   && opt.secondCoordinate === secondCoordinate);

    if (playerCoordinate !== undefined){
      return true;
    }
    else{
      return false;
    }
  }

  public shoot = (firstCoordinate: number, secondCoordinate: number, name: string) => {
    const isValidTurn = this.setTurnPlayer(name);
    if (isValidTurn)
    {
      this.coordinateSend(firstCoordinate, secondCoordinate);
      const players = this.player;
      const player = players.find(opt => opt.name === this.name);
      const opponent = players.find(opt => opt.name !== this.name);
      const opponentCoordinate = opponent.coordinates.find((x: { firstCoordinate: number; secondCoordinate: number; }) =>
      x.firstCoordinate === firstCoordinate && x.secondCoordinate === secondCoordinate);
      if (opponentCoordinate === undefined){
        this.alertService.offTargetAlert();
        this.setCoordinateMiss(firstCoordinate, secondCoordinate);
      }
      else {
        const isValid = this.setCoordinate(firstCoordinate, secondCoordinate);
        if (isValid){
          this.setShootCoordinate(firstCoordinate, secondCoordinate);
          const playerDetails = new Player();
          this.alertService.hitAlert();
          playerDetails.Name = this.name;
          playerDetails.HitPoints = player.hitPoints + this.info.hitPoint;
          this.handlerPlayerInvoke(playerDetails);
        }
      }
    }
    else{
      const players = this.player;
      const opponent = players.find(opt => opt.name !== this.name);
      this.alertService.turnAlert(opponent.name);
    }
  }

  private setCoordinate = (firstCoordinate: number, secondCoordinate: number) => {
    const isValid = this.replayCoordinate(firstCoordinate, secondCoordinate);
    if (isValid){
      const coordinate = new Coordinate();
      coordinate.firstCoordinate = firstCoordinate;
      coordinate.secondCoordinate = secondCoordinate;
      coordinate.name = this.name;
      this.playerCoordinates.push(coordinate);
      return true;
    }
    else{
      this.alertService.replayCoordinate();
      return false;
    }
  }

  private setCoordinateMiss = (firstCoordinate: number, secondCoordinate: number) => {
    const isValid = this.replayCoordinate(firstCoordinate, secondCoordinate);
    if (isValid){
      const coordinate = new Coordinate();
      coordinate.firstCoordinate = firstCoordinate;
      coordinate.secondCoordinate = secondCoordinate;
      coordinate.name = this.name;
      this.coordinateMiss.push(coordinate);
      return true;
    }
    else{
      this.alertService.replayCoordinate();
      return false;
    }
  }

  private replayCoordinate = (firstCoordinate: number, secondCoordinate: number) => {
    const coordinate = this.playerCoordinates.find(opt => opt.name === this.name
                                                && opt.firstCoordinate === firstCoordinate
                                                && opt.secondCoordinate === secondCoordinate);
    if (coordinate === undefined){
      return true;
    }
    else{
      return false;
    }
  }

  private isWinHandler(): boolean{
    const players = this.player;
    const player = players.find(opt => opt.name === this.name);
    if (player.hitPoints === this.info.hitPointMax){
      this.alertService.winAlert();
      return true;
    }
    else{
      return false;
    }
  }

  private isLoseHandler(): boolean{
    const players = this.player;
    const player = players.find(opt => opt.name !== this.name);
    if (player.hitPoints === this.info.hitPointMax){
      this.alertService.loseAlert();
      return true;
    }
    else{
      return false;
    }
  }

  private handlerPlayerInvoke = (playerDetails: Player) => {
    this.connect.addHitPointInvoke(playerDetails);
    this.connect.startConnection();
    this.connect.addTransferDataListener();
  }

  private buildChatMessage(message: string): ChatMessage{
    const chatMessage = new ChatMessage();
    chatMessage.name = this.name;
    chatMessage.text = message;

    return chatMessage;
  }

  public sendMessage = () => {
    const chatMessage = this.buildChatMessage(this.message);
    this.connect.sendMessageToHub(chatMessage).subscribe(_ => {
        this.message = '';
    });
  }
}
