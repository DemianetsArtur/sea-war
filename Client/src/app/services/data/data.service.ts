import { Injectable } from '@angular/core';
import { Coordinate } from 'src/app/models/coordinate/coordinate';
import { Player } from 'src/app/models/player/player';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  public name = '';
  public players: Array<Player> = {} as Array<Player>;
  public coordinateList: Array<Coordinate> = {} as Array<Coordinate>;
  constructor() { }
}
