import { Component, OnInit } from '@angular/core';
import { faUnderline } from '@fortawesome/free-solid-svg-icons';
import { delay } from 'rxjs/operators';
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
  public userAccountCurrentSubscription!: any;
  public userAccountArraySubscription!: any;
  public userAllSubscription!: any;
  public userData = new UserAccount();
  public userAccountCurrentData!:UserAccount;
  public imageDownload: any = {} as any;
  public userAllArray!: UserAccount[];

  constructor(private connect: ConnectService, 
              private optionInfo: OptionsInfoService) {
    this.userAccountSubscription = this.connect.userAccountData$.subscribe(value => {
      this.userData = value;
    });
    this.userAccountCurrentSubscription = this.connect.userGet(this.userData.name);
    if (this.userData !== undefined){
      this.userAccountCurrentSubscription = this.connect.userAccountCurrentValue$.subscribe(value => {
        if (value !== undefined){
          debugger;
          this.userAccountCurrentData = value;
          
        }
      });
    }
    this.connect.userAllGet(this.userData.name);
    this.userAccountArraySubscription = this.connect.userAllGet(this.userData.name);
   }

  ngOnInit(): void {
    this.hubConnect();
    this.userAccountCurrentSubscription = this.connect.userGet(this.userData.name);
    
  }
  
  private hubConnect = () => {
    this.connect.startConnection();
    this.connect.handlerGetUsersInFriendship();
    this.connect.handlerGetUserAll();
    
    this.connect.handlerGetUsersInFriendship();
    this.connect.handlerGetNotificationAddToFriend();

    this.userAccountCurrentSubscription = this.connect.userAll$.subscribe((value) => {
      if (value !== undefined){
        if(value.find(opt => opt.name === this.userData.name)?.imagePath === null){
          debugger;
          delay(5000);
        
        }

        this.userAllArray = value.filter(opt => opt.name === this.userData.name);
        console.log('data: ', this.userAllArray);
      }
    });
  }

  // private hubConnect = () => {
  //   this.connect.startConnection();
  //   this.connect.handlerGetUsersInFriendship();
  //   this.connect.handlerGetNotificationAddToFriend();

  //   if (this.userData !== undefined){
  //     this.userAccountArraySubscription = this.connect.userAccountArray$.subscribe(value => {
  //       this.allUser = value;
  //       this.userArray = this.allUser;
        
  //     });
         
  //   }
  // }

}
