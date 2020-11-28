import { Component, OnInit } from '@angular/core';
import { Coordinate } from 'src/app/models/coordinate/coordinate';
import { AlertHandlerService } from 'src/app/services/alert-handler/handler-alert.service';
import { PlayerChangeRequest } from '../../models/player-change-request/player-change-request';
import { ActivatedRoute, Router } from '@angular/router';
import { InfoOptionsService } from '../../services/info-options/infooptions.service';
import { Player } from 'src/app/models/player/player';
import { UrlService } from 'src/app/services/url/url.service';
import { ConnectService } from '../../services/connect-service/connect.service';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent implements OnInit {
  public name = '';
  public players: Array<Player> = [];
  public coordinateList: Array<Coordinate> = [];
  public nameClient = '';

  constructor(private alertService: AlertHandlerService,
              private route: ActivatedRoute,
              private router: Router,
              private info: InfoOptionsService,
              private connect: ConnectService,
              private url: UrlService) {
  }

  public sizeCol = Array(this.info.sizeBoard);

  ngOnInit(): void {
    this.connect.startConnection();
  }

  public setNameClient(): void{
    if (!this.isShipCountValid()){
      this.alertService.shipCountAlert(this.info.shipCount);
    }
    if (!this.isPlayerCountValid()){
      this.alertService.userCountAlert();
    }
    if (this.isShipCountValid() && this.isPlayerCountValid()){
      const changeRequest = new Player();
      changeRequest.Name = this.nameClient;
      changeRequest.Coordinates = this.coordinateList;
      this.connect.createPlayerService(changeRequest);
      this.coordinateList = [];
      this.router.navigate([this.url.gameBoard, this.nameClient]);
    }
  }

  private isShipCountValid(): boolean{
    const isValid = (this.coordinateList.length > this.info.shipCount
                  || this.coordinateList.length < this.info.shipCount) ? false : true;
    if (!isValid){
      return false;
    }
    else{
      return true;
    }
  }

  private isPlayerCountValid(): boolean{
    const isValid = (this.players.length === this.info.playerSize) ? false : true;
    if (!isValid){
      return false;
    }
    else{
      return true;
    }
  }

  public setStyle = (firstCoordinate: number, secondCoordinate: number) => {
    const playerCoordinate = this.coordinateList.find(opt => opt.firstCoordinate === firstCoordinate
                                                   && opt.secondCoordinate === secondCoordinate);

    if (playerCoordinate !== undefined){
      return true;
    }
    else{
      return false;
    }
  }

  public setCoordinate(firstCoordinate: number, secondCoordinate: number): void{
    const coordinateObj = new Coordinate();
    coordinateObj.firstCoordinate = firstCoordinate;
    coordinateObj.secondCoordinate = secondCoordinate;


    if (!this.isDifferentCoordinates(coordinateObj)){
      this.alertService.coordinateDifferentAlert();
    }
    if (!this.isShipNearby(coordinateObj)){
      this.alertService.shipNearbyAlert();
    }
    if (this.isDifferentCoordinates(coordinateObj)
    && this.isShipNearby(coordinateObj)){
      this.coordinateList.push(coordinateObj);
      this.alertService.successAlert();
    }
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

  private isShipNearby(obj: Coordinate): boolean{
    const firstCurrentCoordinate = obj.firstCoordinate;
    const secondCurrentCoordinate = obj.secondCoordinate;

    const firstLessCoordinate = firstCurrentCoordinate - 1;
    const secondLessCoordinate = obj.secondCoordinate - 1;

    const firstLargerCoordinate = obj.firstCoordinate + 1;
    const secondLargerCoordinate = obj.secondCoordinate + 1;

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
      return false;
    }
    else{
      return true;
    }
  }
}
