import { Component, OnInit } from '@angular/core';
import { UserAccountService } from '../../../services/user-account/user-account.service';

@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css']
})
export class AdminHomeComponent implements OnInit {
  public adminData!: string;
  constructor(private userAccountService: UserAccountService) { }

  ngOnInit(): void {
  }

  public handlerAdminData = () => {
    this.userAccountService.getAdminData().subscribe(
      (result: any) => {
        this.adminData = result;
      }
    );
  }

}
