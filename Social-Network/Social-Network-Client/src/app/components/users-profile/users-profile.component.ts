import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { UserAccount } from 'src/app/models/user-account/user-account';
import { ConnectService } from 'src/app/services/connect/connect.service';

@Component({
  selector: 'app-users-profile',
  templateUrl: './users-profile.component.html',
  styleUrls: ['./users-profile.component.css']
})
export class UsersProfileComponent implements OnInit {
  public userAccountSubscription!: any;
  public userData = new UserAccount();
  private userName!: string;
  private routeSubscription!: Subscription;
  constructor(private connect: ConnectService, 
              private route: ActivatedRoute) { 
    this.route.queryParams.subscribe(opt => {
      this.userName = opt['nickname'];
    });
    this.userAccountSubscription = this.connect.usersGet(this.userName);
    this.connect.userGetData$.subscribe(opt => {
      this.userData = opt;
    });
  }

  ngOnInit(): void {
  }

}
