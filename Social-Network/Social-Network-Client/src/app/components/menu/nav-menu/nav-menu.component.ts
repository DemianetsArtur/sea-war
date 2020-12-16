import { Component, OnInit } from '@angular/core';
import { UserAccount } from '../../../models/user-account/user-account';
import { UserRole } from '../../../models/user-account/user-role';
import { ConnectService } from '../../../services/connect/connect.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  public isExpanded = false;
  public userAccountSubscription!: any;
  public userData = new UserAccount();
  public userRole = UserRole;
  constructor(private connect: ConnectService) {
    this.userAccountSubscription = this.connect.userAccountData$.subscribe(value => {
      this.userData = value;
    });
   }

   public collapse = () => {
     this.isExpanded = false;
   }

   public toggle = () => {
     this.isExpanded = !this.isExpanded;
   }

   public logout = () => {
     this.connect.logout();
   }
 

}
