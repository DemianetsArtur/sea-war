import { catchError, delay, tap } from 'rxjs/operators';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserAccount } from 'src/app/models/user-account/user-account';
import { ConnectService } from 'src/app/services/connect/connect.service';
import { NotificationFriendInfo } from 'src/app/models/notification/notification-add-to-friend/notification-friend-info';
import { NotificationInfoService } from 'src/app/services/notification/notification-info/notification-info.service';
import { Friend } from 'src/app/models/friend/friend';
import { PostInfo } from 'src/app/models/posts/post-info';
import { CommentSend } from 'src/app/models/comments/comment-send-info/comment-send';

@Component({
  selector: 'app-users-profile',
  templateUrl: './users-profile.component.html',
  styleUrls: ['./users-profile.component.css']
})
export class UsersProfileComponent implements OnInit {
  public userAccountSubscription!: any;
  public userCurrentData = new UserAccount();
  public userData = new UserAccount();
  public commentPostSubscription!: any;
  private userName!: string;
  public notificationAddToFriendSubscription!: any;
  public userAccountCurrentSubscription!: any;
  public notificationAddToFriend: NotificationFriendInfo[] = []; 
  public notificationArray!: NotificationFriendInfo[];
  public usersInFriendsSubscription!: any;
  public usersInFriendsArray: Friend[] = [];
  public usersInFriends: Friend[] = [];
  public postsSubscription!: any;
  public postsGet!: PostInfo[];
  public commentText = '';
  public commentPostArray!: CommentSend[];
  public userAccountCurrentData!:UserAccount;

  constructor(private connect: ConnectService, 
              private route: ActivatedRoute, 
              private notificationInfoService: NotificationInfoService) { 
    this.route.queryParams.subscribe(opt => {
      this.userName = opt['nickname'];
    });
    
    this.userAccountSubscription = this.connect.userAccountData$.subscribe(value => {
      this.userCurrentData = value;
    });
    this.userAccountCurrentSubscription = this.connect.userGet(this.userCurrentData.name);
    this.userAccountCurrentSubscription = this.connect.userAccountCurrentValue$.subscribe(value => {
      if (value !== undefined){
        this.userAccountCurrentData = value; 
      }
    });
    this.userAccountSubscription = this.connect.usersGet(this.userName);
    this.connect.userGetData$
                .pipe()
                .subscribe(opt => {
      this.userData = opt;
    });
    this.postsSubscription = this.connect.postsGet(this.userName).subscribe(value => {
      if(value !== undefined){
        this.postsGet = value;
      }
    });
    this.commentPostSubscription = this.connect.commentPostGet(this.userName).subscribe(value => {
      if (value.length !== 0) {
        debugger;
          const comments = value;
          this.commentPostArray = comments.filter(opt => opt.userNameResponse === this.userName);
          console.log('comment-users-main: ', value)
      }
    });
    this.hubConnect();
  }

  ngOnInit(): void {
    this.hubConnect();
  }

  public commentSend = (post: PostInfo, textComment: string) => {
    if(textComment.trim().length === 0) {
      return;
    }
    
    const comment = new CommentSend();
    comment.userName = this.userAccountCurrentData.name;
    comment.userImage = this.userAccountCurrentData.imagePath;
    comment.userNameResponse = post.name;
    comment.contentName = post.nameContent;
    comment.text = textComment;

    this.connect.commentPostCreate(comment)
                .pipe(tap(_ => {
                  this.hubConnect();
                  this.commentPostSubscription = this.connect.commentPostGet(this.userData.name).subscribe(value => {
                    if (value != undefined) {
                        const comments = value;
                        this.commentPostArray = comments.filter(opt => opt.userNameResponse === this.userData.name);
                        console.log('comment: ', value);

                    }
                  });
                }))
                .subscribe();
  }

  public addFriend = (nameResponse: string, nameToResponse: string) => {
    debugger;
    this.userData.isBlock = true;
    const notificationInfo = new NotificationFriendInfo();
    notificationInfo.userNameResponse = nameResponse;
    notificationInfo.userNameToResponse = nameToResponse;
    this.connect.eventAddToFriend(notificationInfo).pipe(tap(data => {
        this.hubConnect();
    }),catchError(async (err) => {
      
    })).subscribe();
  }

  private getNotificationAddToFriend = (nameToResponse: string) => {
    const notificationInfo = new NotificationFriendInfo();
    notificationInfo.userNameToResponse = nameToResponse;
    notificationInfo.userNameResponse = nameToResponse;
    this.connect.getEventAddToFriend(notificationInfo);
    this.notificationAddToFriendSubscription = this.connect.notificationAddToFriends$.subscribe(value => {
      if (value !== undefined) {
        this.notificationArray = value;
        this.notificationAddToFriend = this.notificationArray.filter(name => name.userNameResponse === this.userCurrentData.name && name.nameResponse === this.notificationInfoService.eventAddToFriend);
        if (this.notificationAddToFriend.length !== 0){
          this.setUserBlock();
        }
      }
    });
  }

  private setUserBlock = () => {
      const eventAddToFriends = this.notificationAddToFriend.filter(opt => opt.userNameResponse === this.userCurrentData.name && opt.userNameToResponse === this.userData.name 
                                                                 && opt.nameResponse === this.notificationInfoService.eventAddToFriend);
      if(eventAddToFriends.length !== 0){
        this.userData.isBlock = true;
      }
      else{
        this.userData.isBlock = false;
      }
  }

  private hubConnect = () => {
    this.connect.startConnection();
    this.connect.handlerGetUsersInFriendship();
    this.connect.handlerGetNotificationAddToFriend();
    this.getNotificationAddToFriend(this.userCurrentData.name);
    this.connect.handlerGetCommentPost();

    if (this.userData !== undefined){  
      this.usersInFriendsSubscription = this.connect.usersInFriends$.subscribe(value => {
        this.usersInFriendsArray = value;
        this.usersInFriends = this.usersInFriendsArray.filter(name => name.userNameResponse === this.userCurrentData.name && name.userNameToResponse === this.userData.name );
        if (this.usersInFriendsArray.length !== 0){
          this.setUsersParameters();
        }
      });  
      this.notificationAddToFriendSubscription = this.connect.notificationAddToFriends$.subscribe(value => {
        if (value !== undefined) {
          this.notificationArray = value;
          this.notificationAddToFriend = this.notificationArray.filter(name => name.userNameResponse === this.userCurrentData.name 
                                                                    && name.userNameToResponse === this.userData.name 
                                                                    && name.nameResponse === this.notificationInfoService.eventAddToFriend);
              if (this.notificationAddToFriend.length !== 0){
                this.setUserBlock();
              }
              else{
                if(this.userData.isBlock === true){
                  this.userData.isBlock = false;
                }
              }
        }
      }); 
      
      this.commentPostSubscription = this.connect.commentPost$.subscribe((value) => {
        if(value !== undefined){
          this.commentPostArray = value.filter(opt => opt.userNameResponse === this.userData.name);
        }
      });
    }
  }

  public removeFromFriends = (name: string) => {
    const friend = new Friend();
    friend.userNameResponse = this.userCurrentData.name;
    friend.userNameToResponse = name;
    debugger;
    this.connect.removeFromFriends(friend).pipe(tap(_ => {
      this.removeStatusInFriends(name);
    })).subscribe();
  }

  private setUsersParameters = () => {
      for(const friends of this.usersInFriends){
        if(this.userData.name === friends.userNameToResponse){
          this.userData.isFriends = true;
        }
      }
  }


  private removeStatusInFriends = (name: string) => {
      if (this.userData.name === name) {
        this.userData.isFriends = false;
        this.userData.isBlock = false;
      }
  }
}
