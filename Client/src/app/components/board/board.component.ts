import { Component, OnInit } from '@angular/core';
import { Coordinate } from 'src/app/models/coordinate/coordinate';
import { AlertHandlerService } from 'src/app/services/alert-handler/handler-alert.service';
import { ActivatedRoute, Router } from '@angular/router';
import { InfoOptionsService } from '../../services/info-options/infooptions.service';
import { Player } from 'src/app/models/player/player';
import { UrlService } from 'src/app/services/url/url.service';
import { ConnectService } from '../../services/connect-service/connect.service';
import { ShipInfo } from '../../models/ship-info/ship-info';

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
  private count = 0;
  public shipInfo: ShipInfo = new ShipInfo();
  private isNewShip = true;
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
    if (!this.isPlayerCountValid()){
      this.alertService.userCountAlert();
    }
    if (this.isPlayerCountValid()){
      const changeRequest = new Player();
      changeRequest.Name = this.nameClient;
      changeRequest.Coordinates = this.coordinateList;
      this.connect.createPlayerService(changeRequest);
      this.coordinateList = [];
      this.router.navigate([this.url.gameBoard, this.nameClient]);
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

    if (this.isDifferentCoordinates(coordinateObj)
     && this.isShipN(coordinateObj)
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

    // if (this.coordinateList.length === this.info.coordinateCountMax){
    //   
    // }
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

private isShipN = (coordinateObj: Coordinate) => {
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
    const isValid = this.coordinateList.length === this.info.coordinateCountMax;
    if (isValid){
      this.alertService.shipCountAlert(this.info.shipCreateMax);
      return false;
    }
    else{
      return true;
    }
  }

  private isShipNearby(obj: Coordinate): boolean{
    const firstCurrentCoordinate = obj.firstCoordinate;
    const secondCurrentCoordinate = obj.secondCoordinate;

    const firstLessCoordinate = firstCurrentCoordinate - this.info.coordinate;
    const secondLessCoordinate = obj.secondCoordinate - this.info.coordinate;

    const firstLargerCoordinate = obj.firstCoordinate + this.info.coordinate;
    const secondLargerCoordinate = obj.secondCoordinate + this.info.coordinate;

    const isValid = this.coordinateList.find(opt => opt.firstCoordinate === firstCurrentCoordinate
                                                 && opt.secondCoordinate === secondLessCoordinate
                                                 || opt.firstCoordinate === firstLessCoordinate
                                                 && opt.secondCoordinate === secondCurrentCoordinate
                                                 || opt.firstCoordinate === firstLargerCoordinate
                                                 && opt.secondCoordinate === secondCurrentCoordinate);
    if (isValid){
      return false;
    }
    else{
      return true;
    }
  }
}
