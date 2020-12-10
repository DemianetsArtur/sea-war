import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AlertHandlerService {

  constructor(private toastr: ToastrService) { }

  public turnAlert = (name: string) => {
    this.toastr.warning('Now is the player ' + name + ' turn', 'Oops!');
  }

  public shipCreatedAlert = (name: string, size: number, count: number) => {
    this.toastr.success('Created ' + name + ',' + size + '/' + count, 'Cool');
  }



  public winAlert(): void{
    this.toastr.success('You Win!', 'Hooray!');
  }

  public loseAlert(): void{
    this.toastr.warning('You Lose!', ':(');
  }

  public replayCoordinate(): void{
    this.toastr.warning('You have already got into this ship, choose other coordinates!', 'Oops!');
  }

  public nameRepeat = () => {
    this.toastr.error('There is already a player with that name!', 'Oops!');
  }

  public shipCountAlert(size: number): void{
    this.toastr.warning(`There should be no more than ` + size + ` ships!`, 'Oops!');
  }

  public hitAlert(): void{
    this.toastr.success('you hit your opponent`s ship!', 'Hooray!');
  }

  public offTargetAlert(): void{
    this.toastr.warning('You missed, try again!', 'Oops!');
  }

  public userCountAlert(): void {
    this.toastr.error('No games available!', 'Oops!');
  }

  public coordinateDifferentAlert(): void{
    this.toastr.error('There is already a ship here, choose other coordinates!', 'Oops!');
  }

  public shipNearbyAlert(): void{
    this.toastr.error('Can not place a ship here!', 'Oops!');
  }

  public successAlert(): void{
    this.toastr.success('Ship added!', 'Hooray!');
  }
}
