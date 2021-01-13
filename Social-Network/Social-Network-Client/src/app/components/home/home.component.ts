import { Component, OnInit } from '@angular/core';
import { HubInfoService } from '../../services/hub-info/hub-info.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(public hubInfo: HubInfoService) { }

  ngOnInit(): void {
  }
}
