import {  Component, OnInit, ViewChild,  } from '@angular/core';
import { ActivatedRoute, NavigationStart, Router } from '@angular/router';
import { AlertHandlerService } from 'src/app/services/alert-handler/handler-alert.service';
import { InfoOptionsService } from 'src/app/services/info-options/infooptions.service';
import { UrlService } from 'src/app/services/url/url.service';
import { ConnectService } from '../../services/connect-service/connect.service';
import { Player } from '../../models/player/player';
import { Coordinate } from '../../models/coordinate/coordinate';
import { ChatMessage } from '../../models/chat-message/chat-message';
import { NgxSpinnerService } from 'ngx-spinner';
import { ShipInfo } from 'src/app/models/ship-info/ship-info';
import { NgxAutoScroll } from 'ngx-auto-scroll';
import { BoardComponent } from '../board/board.component';
import { Subscription, BehaviorSubject, Observable } from 'rxjs';

@Component({
  selector: 'app-game-play',
  templateUrl: './game-play.component.html',
  styleUrls: ['./game-play.component.css'],

})
export class GamePlayComponent implements OnInit  {
  public name: any;
  public player: any[] = {} as any[];
  public isPlayGame!: boolean;
  public isGameEnd!: boolean;
  public msgGameEnd!: string;
  private playerCoordinates: Array<Coordinate> = [];
  public message = '';
  public messages: ChatMessage[] = [];
  public coordinates: Coordinate[] = [];
  public turnPlayer!: boolean;
  public turn = 0;
  public isOpponentShoot = false;
  private coordinateShoot: Array<Coordinate> = [];
  private coordinateList: Array<Coordinate> = [];
  private coordinateMiss: Array<Coordinate> = [];
  private isNewShip = true;
  public shipInfo: ShipInfo = new ShipInfo();
  private count = 0;
  public coordinateCount!: boolean;
  public filledPlayersCoordinate!: boolean;
  public playerNameTurn!: string;
  public refreshGame = false;


  constructor(private info: InfoOptionsService,
              private url: UrlService,
              private router: Router,
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
    this.connect.addCoordinatePlayerListener();
    this.connect.PlayerData$.subscribe(opt => {
      this.player = opt;
      this.isPlayGame = (this.player.length === this.info.playerSize) ? true : false;
      if (this.player.length === this.info.playerSize){
        this.isWinHandler();
        this.fillesPlayersCoordinates();
        this.setNamePlayerTurn();
      }
    });
    this.messages = this.connect.messages;
    this.coordinates = this.connect.coordinates;
    this.spinner.show();
  }

  private fillesPlayersCoordinates = () => {
    const player = this.player.find(opt => opt.name === this.name);
    const opponent = this.player.find(opt => opt.name !== this.name);
    this.filledPlayersCoordinate = (player.coordinates.length === this.info.coordinateCountMax
                                 && opponent.coordinates.length === this.info.coordinateCountMax ) ? true : false;
  }

  public playAgainHandler = () => {
    const player = this.player.find(opt => opt.name === this.name);
    this.connect.sendPlayersToHub(player);
    this.refreshGame = true;
    this.connect.addPlayerRemoveListener();
  }

  public isFillCoordinatePlayers = () => {
    const player = this.player.find(opt => opt.name === this.name);
    const opponent = this.player.find(opt => opt.name !== this.name);
    return (player.coordinates.length === this.info.coordinateCountMax
         && opponent.coordinates.length !== this.info.coordinateCountMax) ? true : false;
  }

  private coordinateSend = (firstCoordinate: number, secondCoordinate: number) => {
    const coordinate = new Coordinate();
    coordinate.name = this.name;
    coordinate.firstCoordinate = firstCoordinate;
    coordinate.secondCoordinate = secondCoordinate;
    this.connect.sendCoordinate(coordinate).subscribe();
  }

  private setNamePlayerTurn = () => {
    const player = this.player[0];
    const opponentRes = this.player.find(opt => opt.name !== player.name);
    if (this.playerNameTurn === undefined){
      this.playerNameTurn = player.name;
    }
    else{
      if (player.count === this.info.turnFirstPlayer){
        this.playerNameTurn = player.name;
      }
      else if (opponentRes.count === this.info.turnSecondPlayer){
        this.playerNameTurn = opponentRes.name;
      }
    }
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

  private handlerShootPlayer = (name: string) => {
    const playerDetails = new Player();
    const player = this.player[0];
    const opponentRes = this.player.find(opt => opt.name !== player.name);
    if (name === player.name){
      playerDetails.Name = player.name;
      playerDetails.Count = this.info.turnFirstPlayer;
      this.connect.sendCountToHub(playerDetails);
      this.handlerShootOpponent(opponentRes.name, this.info.turnFirstPlayer);
    }
    else if (name === opponentRes.name){
      playerDetails.Name = opponentRes.name;
      playerDetails.Count = this.info.turnSecondPlayer;
      this.connect.sendCountToHub(playerDetails);
      this.handlerShootOpponent(player.name, this.info.turnSecondPlayer);
    }

  }

  private handlerCountChange = (name: string) => {
    const playerDetails = new Player();
    playerDetails.Name = name;
    playerDetails.Count = this.turn;
    this.connect.sendCountToHub(playerDetails);
  }

  private handlerShootOpponent = (name: string, turn: number) => {
    const playerDetails = new Player();
    playerDetails.Name = name;
    playerDetails.Count = turn;
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
    this.coordinateShoot.push(coordinate);
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
    const playerCoordinate = this.coordinateShoot.find(opt => opt.name === this.name
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
          this.handlerShootPlayer(name);
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

  private isWinHandler = () => {
    const player = this.player.find(opt => opt.name === this.name);
    const opponentRes = this.player.find(opt => opt.name !== player.name);
    if (player.hitPoints === this.info.hitPointMax && opponentRes.hitPoints !== this.info.hitPointMax){
      this.isGameEnd = true;
      this.msgGameEnd = this.info.msgWin;
    }
    else if (player.hitPoints !== this.info.hitPointMax && opponentRes.hitPoints === this.info.hitPointMax){
      this.isGameEnd = true;
      this.msgGameEnd = this.info.msgLose;
    }
    else{
      this.isGameEnd = false;
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

  public setStyle = (firstCoordinate: number, secondCoordinate: number) => {
    const player = this.player.find(opt => opt.name === this.name);
    if (player.coordinates.length === this.info.countEmpty){
      const playerCoordinate = this.coordinateList.find(opt => opt.firstCoordinate === firstCoordinate
        && opt.secondCoordinate === secondCoordinate);

      if (playerCoordinate !== undefined){
        return true;
      }
      else{
        return false;
      }
    }

    else{
      const playerCoordinate = player.coordinates.find((opt: { firstCoordinate: number; secondCoordinate: number; }) =>
                                                        opt.firstCoordinate === firstCoordinate
                                                     && opt.secondCoordinate === secondCoordinate);
      if (playerCoordinate !== undefined){
          return true;
      }
      else{
        return false;
      }
    }
  }


  public sendCoordinatePlayer = () => {
    const changeRequest = new Player();
    changeRequest.Name = this.name;
    changeRequest.Coordinates = this.coordinateList;
    this.connect.sendCoordinatesPlayer(changeRequest);
  }


  public setCoordinatePlayer(firstCoordinate: number, secondCoordinate: number): void{
    const coordinateObj = new Coordinate();
    coordinateObj.firstCoordinate = firstCoordinate;
    coordinateObj.secondCoordinate = secondCoordinate;

    if (!this.isDifferentCoordinates(coordinateObj)){
      this.alertService.coordinateDifferentAlert();
    }

    if (this.isDifferentCoordinates(coordinateObj)
     && this.isShipNew(coordinateObj)
     && this.isValidShipCount()){
      this.shipCreate(coordinateObj);
    }
  }

  private shipCreate = (coordinateObj: Coordinate) => {
    const battleship = this.shipInfo.battleship;
    const cruiser = this.shipInfo.cruiser;
    const destroyer = this.shipInfo.destroyer;
    const torpedoBoat = this.shipInfo.torpedoBoat;
    this.count += this.info.countCreateShip;
    if (battleship.isState){
          coordinateObj.name = battleship.name;
          this.shipInfo.battleship.coordinates.push(coordinateObj);
          this.coordinateList.push(coordinateObj);
          this.isNewShip = false;
          if (battleship.coordinates.length === battleship.size){
            this.shipInfo.battleship.isStateCount += this.info.countCreateShip;
            this.alertService.shipCreatedAlert(battleship.name, battleship.isStateCount, battleship.count);
          }
          if (battleship.isStateCount === battleship.count){

              this.shipInfo.battleship.isState = false;
              this.count = this.info.countEmpty;
              this.isNewShip = true;
          }
    }

    if (!battleship.isState && this.count !== this.info.countEmpty && cruiser.isState){
      const isValid = (cruiser.coordinates.length === this.info.countEmpty) ? true : false;
      if (this.shipNear(coordinateObj, isValid)){
        this.shipInfo.cruiser.coordinates.push(coordinateObj);
        this.coordinateList.push(coordinateObj);
        this.isNewShip = false;
      }
      if (cruiser.coordinates.length === cruiser.size){
        this.shipInfo.cruiser.isStateCount += this.info.countCreateShip;
        this.shipInfo.cruiser.coordinates = [];
        this.isNewShip = true;
        this.alertService.shipCreatedAlert(cruiser.name, cruiser.isStateCount, cruiser.count);
      }
      if (cruiser.isStateCount === cruiser.count){
        this.shipInfo.cruiser.isState = false;
        this.count = this.info.countEmpty;
        this.isNewShip = true;
      }
    }

    if (this.count !== this.info.countEmpty && !cruiser.isState && destroyer.isState){
      const isValid = (destroyer.coordinates.length === this.info.countEmpty) ? true : false;
      if (this.shipNear(coordinateObj, isValid)){
        this.shipInfo.destroyer.coordinates.push(coordinateObj);
        this.coordinateList.push(coordinateObj);
        this.isNewShip = false;
      }
      if (destroyer.coordinates.length === destroyer.size){
        this.shipInfo.destroyer.isStateCount += this.info.countCreateShip;
        this.shipInfo.destroyer.coordinates = [];
        this.isNewShip = true;
        this.alertService.shipCreatedAlert(destroyer.name, destroyer.isStateCount, destroyer.count);
      }
      if (destroyer.isStateCount === destroyer.count){
        this.shipInfo.destroyer.isState = false;
        this.count = this.info.countEmpty;
        this.isNewShip = true;
      }
    }

    if (this.count !== this.info.countEmpty && !destroyer.isState && torpedoBoat.isState){
      const isValid = (torpedoBoat.coordinates.length === this.info.countEmpty) ? true : false;
      if (this.shipNear(coordinateObj, isValid)){
        this.shipInfo.torpedoBoat.coordinates.push(coordinateObj);
        this.coordinateList.push(coordinateObj);
        this.isNewShip = false;
      }
      if (torpedoBoat.coordinates.length === torpedoBoat.size){
        this.shipInfo.torpedoBoat.isStateCount += this.info.countCreateShip;
        this.shipInfo.torpedoBoat.coordinates = [];
        this.isNewShip = true;
        this.alertService.shipCreatedAlert(torpedoBoat.name, torpedoBoat.isStateCount, torpedoBoat.count);
      }
      if (torpedoBoat.isStateCount === torpedoBoat.count){
        this.shipInfo.torpedoBoat.isState = false;
        this.count = this.info.countEmpty;
        this.isNewShip = true;
      }
    }
    this.coordinateCount = (this.coordinateList.length === this.info.coordinateCountMax) ? true : false;
  }
  private isDifferentCoordinates(obj: Coordinate): boolean{
    const isValid = this.coordinateList.find(opt =>
       opt.firstCoordinate === obj.firstCoordinate &&
       opt.secondCoordinate === obj.secondCoordinate );

    if (isValid){
      return false;
    }
    else{
      return true;
    }
  }

  private isValidShipCount = () => {
    const player = this.player.find(opt => opt.name === this.name);
    const isValid = (this.coordinateList.length === this.info.coordinateCountMax
                  || player.coordinates.length === this.info.coordinateCountMax);
    if (isValid){
      this.alertService.shipCountAlert(this.info.shipCreateMax);
      return false;
    }
    else{
      return true;
    }
  }

  private isShipNew = (coordinateObj: Coordinate) => {
    const firstCoordinateMore = coordinateObj.firstCoordinate + this.info.coordinate;
    const secondCoordinateMore = coordinateObj.secondCoordinate + this.info.coordinate;
    const firstCoordinateLess = coordinateObj.firstCoordinate - this.info.coordinate;
    const secondCoordinateLess = coordinateObj.secondCoordinate - this.info.coordinate;
    const firstCoordinateCurrent = coordinateObj.firstCoordinate;
    const secondCoordinateCurrent = coordinateObj.secondCoordinate;
    const coordinates = this.coordinateList.find(opt => opt.firstCoordinate === firstCoordinateCurrent
                                              && opt.secondCoordinate === secondCoordinateMore
                                              || opt.firstCoordinate === firstCoordinateMore
                                              && opt.secondCoordinate === secondCoordinateCurrent
                                              || opt.firstCoordinate === firstCoordinateCurrent
                                              && opt.secondCoordinate === secondCoordinateLess
                                              || opt.firstCoordinate === firstCoordinateLess
                                              && opt.secondCoordinate === secondCoordinateCurrent);
    if (coordinates || this.isNewShip){
      return true;
    }
    else{
      this.alertService.shipNearbyAlert();
      return false;
    }
  }

  private shipNear = (obj: Coordinate, isFirst: boolean) => {
    if (isFirst){
      const firstCurrentCoordinate = obj.firstCoordinate;
      const secondCurrentCoordinate = obj.secondCoordinate;
      const firstLessCoordinate = firstCurrentCoordinate - this.info.coordinate;
      const secondLessCoordinate = obj.secondCoordinate - this.info.coordinate;
      const firstLargerCoordinate = obj.firstCoordinate + this.info.coordinate;
      const secondLargerCoordinate = obj.secondCoordinate + this.info.coordinate;
      const isValid = this.coordinateList.find(opt =>
        opt.firstCoordinate === firstLessCoordinate
        && opt.secondCoordinate === secondLessCoordinate
        || opt.firstCoordinate === firstLessCoordinate
        && opt.secondCoordinate === secondCurrentCoordinate
        || opt.firstCoordinate === firstLessCoordinate
        && opt.secondCoordinate === secondLargerCoordinate
        || opt.firstCoordinate === firstCurrentCoordinate
        && opt.secondCoordinate === secondLargerCoordinate
        || opt.firstCoordinate === firstLargerCoordinate
        && opt.secondCoordinate === secondLargerCoordinate
        || opt.firstCoordinate === firstLargerCoordinate
        && opt.secondCoordinate === secondCurrentCoordinate
        || opt.firstCoordinate === firstLargerCoordinate
        && opt.secondCoordinate === secondLessCoordinate
        || opt.firstCoordinate === firstCurrentCoordinate
        && opt.secondCoordinate === secondLessCoordinate);
      if (isValid){
        this.alertService.shipNearbyAlert();
        return false;
      }
      else{
        return true;
      }
    }
    else{
      return true;
    }
  }
}
