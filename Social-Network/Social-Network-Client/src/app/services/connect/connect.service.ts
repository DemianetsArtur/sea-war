import { UserAccountRegister } from './../../models/user-account/user-account-register/user-account-register';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { OptionsInfoService } from '../options-info/options-info.service';
import { map } from 'rxjs/operators';
import { UserAccount } from '../../models/user-account/user-account';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { UserRole } from '../../models/user-account/user-role';

@Injectable({
  providedIn: 'root'
})
export class ConnectService {
  public userAccountData = new BehaviorSubject<UserAccount>(new UserAccount());
  public userAccountData$ = this.userAccountData.asObservable();
  constructor(private http: HttpClient, 
              private optionsInfo: OptionsInfoService, 
              private router: Router) { }

  public userDataConnect = () => {
    return this.http.get(this.optionsInfo.getUserData).pipe(map(result => result));
  }

  public adminDataConnect = () => {
    return this.http.get(this.optionsInfo.getAdminData).pipe(map(result => result));
  }

  public loginPost = (userDetailsInfo: UserAccount) => {
    return this.http
               .post<any>(this.optionsInfo.loginPost, userDetailsInfo)
               .pipe(map(response => {
                 localStorage.setItem(this.optionsInfo.authToken, response.token);
                 this.setUserAccountDetail();
                 return response;
               }));
  }

  public setUserAccountDetail = () => {
    if (localStorage.getItem(this.optionsInfo.authToken)) {
      const userAccountDetails = new UserAccount();
      const decodeUserAccountDetails = JSON.parse(window.atob(localStorage.getItem(this.optionsInfo.authToken)!.split('.')[1]));
      userAccountDetails.email = decodeUserAccountDetails.sub;
      userAccountDetails.name = decodeUserAccountDetails.firstName;
      userAccountDetails.isLoggedIn = true;
      userAccountDetails.role = decodeUserAccountDetails.role;
      this.userAccountData.next(userAccountDetails);
    }
  }

  public registerPost = (userRegisterDetails: UserAccountRegister) : Observable<UserAccountRegister> => {
    userRegisterDetails.userType = UserRole.User;
    return this.http.post<any>(this.optionsInfo.registerPost, userRegisterDetails);
  }

  public logout = () => {
    localStorage.removeItem(this.optionsInfo.authToken);
    this.router.navigate([this.optionsInfo.loginPath]);
    this.userAccountData.next(new UserAccount());
  }
}
