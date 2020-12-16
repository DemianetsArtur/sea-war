import { Component } from '@angular/core';
import { ConnectService } from './services/connect/connect.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Social-Network-Client';
  constructor (private connect: ConnectService) {
    this.connect.setUserAccountDetail();
  }
}
