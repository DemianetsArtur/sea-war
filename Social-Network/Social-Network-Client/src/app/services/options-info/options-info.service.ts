import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class OptionsInfoService {
  public host!: string;
  public getUserData!: string;
  public getAdminData!:string;
  public loginPost!: string;
  public authToken!: string;
  public loginPath!: string;
  public returnUrl!: string;
  public registerPost!: string;
  public header!: Headers;

  constructor() {
    this.host = 'https://localhost:44391/';
    this.getUserData = this.host + 'api/user/get-user-data';
    this.getAdminData = this.host + 'api/user/get-admin-data';
    this.loginPost = this.host + 'api/useraccount/login';
    this.authToken = 'authToken';
    this.loginPath = '/login';
    this.returnUrl = 'returnUrl';
    this.registerPost = this.host + 'api/useraccount/register';
    this.header = new Headers({'Content-Type': 'application/json'});
  }
}
