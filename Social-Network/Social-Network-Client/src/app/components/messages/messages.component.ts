import { Component, OnInit } from '@angular/core';
import { UserAccount } from 'src/app/models/user-account/user-account';
import { ConnectService } from 'src/app/services/connect/connect.service';
import { DomSanitizer } from '@angular/platform-browser';
import { MessageInfo } from '../../models/message/message-info';
import { MessageGet } from 'src/app/models/message/message-get';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  public userAccountSubscription!: any;
  public userAccountArraySubscription!: any;
  public userAccountCurrentSubscription!: any;
  public messageAllSubscription!: any;
  public userAccountCurrentData = new UserAccount();
  public userData = new UserAccount();
  public userArray!: UserAccount[];
  public allUser!: any[];
  public messageInfo = new MessageInfo();
  public userText = '';
  public messageAllArray!: MessageGet[];

  constructor(private connect: ConnectService) {
    this.userAccountSubscription = this.connect.userAccountData$.subscribe(value => {
      this.userData = value;
    });
    this.userAccountArraySubscription = this.connect.userAllGet(this.userData.name);
    this.userAccountArraySubscription = this.connect.userAccountArray$.subscribe(value => {
      this.allUser = value;
      this.userArray = this.allUser; 
    });
    this.userAccountCurrentSubscription = this.connect.userGet(this.userData.name);
    if (this.userData !== undefined){
      this.userAccountCurrentSubscription = this.connect.userAccountCurrentValue$.subscribe(value => {
        this.userAccountCurrentData = value;
      });
    }
  }

  ngOnInit(): void {
  }

  public selectUserForMessage = (user: UserAccount) => {
    this.messageInfo.userNameResponse = this.userAccountCurrentData.name;
    this.messageInfo.userNameToResponse = user.name;
    this.messageInfo.userImageResponse = this.userAccountCurrentData.imagePath;
    this.messageInfo.userImageToResponse = user.imagePath;

    this.hubConnect();
  }

  public sendMessage = () => {
    this.messageInfo.text = this.userText;
    debugger;
    this.connect.messagePost(this.messageInfo).subscribe(_ => {
      this.userText = '';
      this.messageAllGet(this.messageInfo);
    });
  }

  private messageAllGet = (messageInfo: MessageInfo) => {
    if (messageInfo !== undefined){
      this.connect.messageAllGet(messageInfo).subscribe();
      this.messageAllSubscription = this.connect.messageAll$.subscribe(value => {
      if (value !== undefined) {
        this.messageAllArray = value;
        console.log("message ", value);
      }
    });
    }
    debugger;
  }

  private hubConnect = () =>{
    this.connect.startConnection();
    this.connect.handlerGetMessageAll();
    this.messageAllGet(this.messageInfo);
  }

}
