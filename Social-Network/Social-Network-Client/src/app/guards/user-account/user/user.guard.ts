import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserAccount } from '../../../models/user-account/user-account';
import { ConnectService } from '../../../services/connect/connect.service';
import { OptionsInfoService } from '../../../services/options-info/options-info.service';
import { UserRole } from '../../../models/user-account/user-role';

@Injectable({
  providedIn: 'root'
})
export class UserGuard implements CanActivate {
  public userAccountSubscription: any;
  public userData = new UserAccount();

  constructor(private router: Router,
              private connect: ConnectService, 
              private optionsInfo: OptionsInfoService) {
    this.userAccountSubscription = this.connect.userAccountData$.subscribe(value => {
      this.userData = value;
    });
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if (this.userData.role === UserRole.User) {
        return true;
      }

      this.router.navigate([this.optionsInfo.loginPath], {queryParams: { returnUrl: state.url }});
      return false;
  }
  
}
