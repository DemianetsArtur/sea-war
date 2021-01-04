import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  constructor(private toastr: ToastrService) { }

  public sameUserAlert = () => {
    this.toastr.error('','User with the same name already exists!');
  }

  public userNotExist = () => {
    this.toastr.error('', 'No such user exists!');
  }

  public userNickNameNotExist = () => {
    this.toastr.error('', 'User with this nickname does not exist!');
  }

  public imageChange = () => {
    this.toastr.success('', 'You have successfully changed the image!');
  }

  public imageTypeInvalid = () => {
    this.toastr.error('', 'Invalid Image Type!');
  }

  public imageTypeValid = () => {
    this.toastr.success('', 'Valid Image Type!');
  }

  public userMessagesInvalid = () => {
    this.toastr.warning('', 'No messages with this User!');
  }
}
