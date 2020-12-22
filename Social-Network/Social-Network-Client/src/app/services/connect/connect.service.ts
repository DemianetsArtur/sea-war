import { UserAccountRegister } from './../../models/user-account/user-account-register/user-account-register';
import { Injectable } from '@angular/core';
import { HttpClient, HttpEventType, HttpHeaders } from '@angular/common/http';
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
  public fileToUpload!: FormData; 
  public imageDownload!: any;
  public userAccountArray = new BehaviorSubject<UserAccount[]>([]);
  public userAccountArray$ = this.userAccountArray.asObservable();
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
      userAccountDetails.firstName = decodeUserAccountDetails.firstName;
      userAccountDetails.lastName = decodeUserAccountDetails.lastName;
      userAccountDetails.aboutMe = decodeUserAccountDetails.aboutMe;
      userAccountDetails.date = decodeUserAccountDetails.date;
      userAccountDetails.imagePath = decodeUserAccountDetails.imagePath;
      this.userAccountData.next(userAccountDetails);
    }
  }

  public registerPost = (userRegisterDetails: UserAccountRegister) : Observable<UserAccountRegister> => {
    userRegisterDetails.userType = UserRole.User;
    return this.http.post<any>(this.optionsInfo.registerPost, userRegisterDetails);
  }

  public imagePost = (files: any, name: string) => {
    
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append(fileToUpload.type ,fileToUpload, name);

    this.http.post(this.optionsInfo.imagePost, formData, { reportProgress: true, observe: 'events' })
             .subscribe();
  }

  public imageDownloadGet = (name: string) => {
    return this.http.get(this.optionsInfo.imageGet + '/' + name)
               .pipe(map(result => result));

  }

  public userAllGet = (name: string) => {
    return this.http.get<UserAccount[]>(this.optionsInfo.userAllGet + '/' + name).subscribe(value => {
      if (value !== undefined){
        this.userAccountArray.next(value);
      }
      debugger;
    });
  }

  public logout = () => {
    localStorage.removeItem(this.optionsInfo.authToken);
    this.router.navigate([this.optionsInfo.loginPath]);
    this.userAccountData.next(new UserAccount());
  }
}
