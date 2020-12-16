import { Component, OnInit } from '@angular/core';
import { UserAccountService } from '../../../services/user-account/user-account.service';

@Component({
  selector: 'app-user-home',
  templateUrl: './user-home.component.html',
  styleUrls: ['./user-home.component.css']
})
export class UserHomeComponent implements OnInit {
  public userData!: string;
  constructor(private userAccountService: UserAccountService) { }

  ngOnInit(): void {
  }

  public handlerUserData = () => {
    this.userAccountService.getUserData().subscribe(
      (result: any) => {
        this.userData = result;    
      } 
    );
  }
}
