import { Component, OnInit } from '@angular/core';
import { UserAccount } from 'src/app/models/user-account/user-account';
import { ConnectService } from 'src/app/services/connect/connect.service';
import { OptionsInfoService } from 'src/app/services/options-info/options-info.service';
import { faTimes } from '@fortawesome/free-solid-svg-icons';
import { AlertService } from '../../../services/alert/alert.service';

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
  public allUser!: any[];
  public faTimes = faTimes;
  constructor(private connect: ConnectService, 
              private optionInfo: OptionsInfoService, 
              private alert: AlertService) {
    this.userAccountSubscription = this.connect.userAccountData$.subscribe(value => {
      this.userData = value;
    });
    this.userAccountArraySubscription = this.connect.userAllGet(this.userData.name);
    if (this.userData !== undefined){
    this.userAccountArraySubscription = this.connect.userAccountArray$.subscribe(value => {
      this.allUser = value;
      this.userArray = this.allUser;
    });
    
  }
  }

  ngOnInit(): void {
  }

  public filterData = (valueFilter: string) => {
    if (valueFilter === ""){
      this.userArray = this.allUser;
    }
    else{
      const value = valueFilter.toLocaleLowerCase();
      this.userArray = this.allUser.filter(val => val.name.toLowerCase() === value);
      if (this.userArray.length === 0) {
          this.alert.userNickNameNotExist();
      }
    }
  }

  public removeFilterData = () => {
    this.userArray = this.allUser;
  }

}
