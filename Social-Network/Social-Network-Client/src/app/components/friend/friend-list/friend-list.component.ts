import { Component, OnInit } from '@angular/core';
import { UserAccount } from 'src/app/models/user-account/user-account';
import { ConnectService } from 'src/app/services/connect/connect.service';
import { OptionsInfoService } from 'src/app/services/options-info/options-info.service';

@Component({
  selector: 'app-friend-list',
  templateUrl: './friend-list.component.html',
  styleUrls: ['./friend-list.component.css']
})
export class FriendListComponent implements OnInit {
  public userAccountSubscription!: any;
  public userAccountArraySubscription!: any;
  public userData = new UserAccount();
  public userArray!: any[];
  constructor(private connect: ConnectService, 
              private optionInfo: OptionsInfoService) {
    this.userAccountSubscription = this.connect.userAccountData$.subscribe(value => {
      this.userData = value;
    });
    this.userAccountArraySubscription = this.connect.userAllGet(this.userData.name);
    if (this.userData !== undefined){
    this.userAccountArraySubscription = this.connect.userAccountArray$.subscribe(value => {
      this.userArray = value;
    });
  }
    debugger;
  }

  ngOnInit(): void {
  }

}
