import { CommentSend } from './../../models/comments/comment-send-info/comment-send';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { delay, tap } from 'rxjs/operators';
import { UserAccount } from 'src/app/models/user-account/user-account';
import { ConnectService } from '../../services/connect/connect.service';
import { OptionsInfoService } from '../../services/options-info/options-info.service';
import { PostInfo } from 'src/app/models/posts/post-info';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  public userAccountSubscription!: any;
  public imageSubscription!: any;
  public userAccountCurrentSubscription!: any;
  public userAccountArraySubscription!: any;
  public commentPostSubscription!: any;
  public postsSubscription!: any;
  public userAllSubscription!: any;
  public userData = new UserAccount();
  public userAccountCurrentData!:UserAccount;
  public imageDownload: any = {} as any;
  public userAllArray!: UserAccount[];
  public postsGet!: PostInfo[];
  public commentText = '';
  public commentPostArray!: CommentSend[];


  constructor(private connect: ConnectService, 
              private optionInfo: OptionsInfoService,
              private router: Router) {
    this.userAccountSubscription = this.connect.userAccountData$.subscribe(value => {
      this.userData = value;
    });
    this.userAccountCurrentSubscription = this.connect.userGet(this.userData.name);
    if (this.userData !== undefined){
      this.userAccountCurrentSubscription = this.connect.userAccountCurrentValue$.subscribe(value => {
        if (value !== undefined){
          this.userAccountCurrentData = value; 
        }
      });

      this.postsSubscription = this.connect.postsGet(this.userData.name).subscribe(value => {
        if(value !== undefined){
          this.postsGet = value;
        }
      });

      this.commentPostSubscription = this.connect.commentPostGet(this.userData.name).subscribe(value => {
        if (value != undefined) {
            const comments = value;
            this.commentPostArray = comments.filter(opt => opt.userNameResponse === this.userData.name);
            console.log('comment: ', value)
        }
      });
    }
    this.connect.userAllGet(this.userData.name);
    this.userAccountArraySubscription = this.connect.userAllGet(this.userData.name);
   }

  ngOnInit(): void {
    this.hubConnect();
    this.userAccountCurrentSubscription = this.connect.userGet(this.userData.name);
    
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
                  this.commentPostSubscription = this.connect.commentPostGet(this.userData.name).subscribe(value => {
                    this.hubConnect();
                    if (value != undefined) {
                        const comments = value;
                        this.commentPostArray = comments.filter(opt => opt.userNameResponse === this.userData.name);
                        console.log('comment: ', value)
                    }
                  });
                }))
                .subscribe();
  }
  
  public postCreate = () => {
    this.router.navigate([this.optionInfo.postCreatePath]);
  }

  private hubConnect = () => {
    this.connect.startConnection();
    this.connect.handlerGetUsersInFriendship();
    this.connect.handlerGetUserAll();
    
    this.connect.handlerGetUsersInFriendship();
    this.connect.handlerGetNotificationAddToFriend();
    this.connect.handlerGetCommentPost();
    
    this.userAccountCurrentSubscription = this.connect.userAll$.subscribe((value) => {
      if (value !== undefined){
        if(value.find(opt => opt.name === this.userData.name)?.imagePath === null){
          delay(5000);  
          this.hubConnect();
        }
        delay(2000);
        this.userAllArray = value.filter(opt => opt.name === this.userData.name);
        console.log('data: ', this.userAllArray);
      }
    });

    this.commentPostSubscription = this.connect.commentPost$.subscribe((value) => {
      if(value !== undefined){
        this.commentPostArray = value.filter(opt => opt.userNameResponse === this.userData.name);
      }
    });
    
  }

}
