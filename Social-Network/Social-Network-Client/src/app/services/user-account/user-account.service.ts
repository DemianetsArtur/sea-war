import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ConnectService } from '../connect/connect.service';

@Injectable({
  providedIn: 'root'
})
export class UserAccountService {

  myAppUrl = '';
  constructor(private connect: ConnectService) { }

  public getUserData = () => {
    return this.connect.userDataConnect();
  }

  public getAdminData = () => {
    return this.connect.adminDataConnect();
  }
}
