import { Component, OnInit } from '@angular/core';

import { UserAccount } from 'src/app/models/user-account/user-account';
import { UserRole } from 'src/app/models/user-account/user-role';
import { ConnectService } from '../../services/connect/connect.service';
import { OptionsInfoService } from '../../services/options-info/options-info.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  public userAccountSubscription!: any;
  public imageSubscription!: any;
  public userData = new UserAccount();
  public userRole = UserRole;
  public imageDownload: any = {} as any;

  constructor(private connect: ConnectService, 
              private optionInfo: OptionsInfoService) {
    this.userAccountSubscription = this.connect.userAccountData$.subscribe(value => {
      this.userData = value;
    });
    if (this.userData !== undefined){
      this.imageSubscription = this.connect.imageDownloadGet(this.userData.name).subscribe(value => {
        this.imageDownload = value + this.optionInfo.blobConfig;
      });
    }

   }

  ngOnInit(): void {
  }

}
