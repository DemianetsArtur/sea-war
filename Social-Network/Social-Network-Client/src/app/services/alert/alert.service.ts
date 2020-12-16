import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  constructor(private toastr: ToastrService) { }

  public sameUserAlert = () => {
    this.toastr.error('','User with the same name already exists');
  }

  public userNotExist = () => {
    this.toastr.error('', 'No such user exists');
  }
}
