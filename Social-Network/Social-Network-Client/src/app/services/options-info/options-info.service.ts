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
  public imagePost!: string;
  public imageGet!: string;
  public blobConfig!: string;
  public userAllGet!: string;
  public editPath!: string;
  public userGet!: string;
  public editPost!: string;

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
    this.imagePost = this.host + 'api/useraccount/image-upload';
    this.imageGet = this.host + 'api/useraccount/getfile';
    this.blobConfig = '?sv=2019-12-12&ss=bqtf&srt=sco&sp=rwdlacuptfx&se=2020-12-22T16:12:04Z&sig=bm62tPLhiEyYlyRQDR9KL8VqZxSnXIoSF4%2BKwyAp8DI%3D&_=1608626588679';
    this.userAllGet = this.host + 'api/friend/get-user-all';
    this.editPath = '/edit-user';
    this.userGet = this.host + 'api/useraccount/user-get';
    this.editPost = this.host + 'api/edit/user-edit';
  }
}
