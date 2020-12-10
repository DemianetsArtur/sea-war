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
  public nameList: Player[] = [];
  public shipInfo: ShipInfo = new ShipInfo();
  public player: any[] = {} as any[];
  public playerCount!: boolean;
  public nameRepeat!: Player;
  private isNameRepeat!: boolean;

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
    this.connect.addTransferNameListener();
    this.connect.addPlayerRemoveListener();
    this.connect.addTransferDataListener();
    this.connect.PlayerData$.subscribe(opt => {
      this.player = opt;
      this.playerCount = (this.player.length === this.info.playerSize) ? true : false;
    });
    this.nameRepeat = this.connect.names;
    this.isNameRepeat = (this.nameRepeat === null) ? true : false;
    if (this.isNameRepeat){
      this.alertService.nameRepeat();
      this.isNameRepeat = false;
    }
  }


  public setNameClient(): void{

    if (this.isPlayerCountValid() && !this.isNameRepeatHandler()){
      const changeRequest = new Player();
      changeRequest.Name = this.nameClient;
      this.connect.sendNameToHub(changeRequest);
      this.connect.createPlayerService(changeRequest);
      this.router.navigate([this.url.gameBoard, this.nameClient]);
    }
    else{
    }
  }

  private isPlayerCountValid = () =>{
    const isValid = this.playerCount;
    if (!isValid){
      return true;
    }
    else{
      this.alertService.userCountAlert();
      return false;
    }
  }

  private isNameRepeatHandler = () => {
    const isValid = this.isNameRepeat;
    if (isValid){
      
      this.alertService.nameRepeat();
      this.isNameRepeat = false;
      return true;
    }
    else{
      return false;
    }
  }
}
