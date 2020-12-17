import { Component, OnInit } from '@angular/core';
import { faUser, faPencilAlt, faComment, faUsers, faBell } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  public faUser = faUser;
  public faPencilAlt = faPencilAlt;
  public faComment = faComment;
  public faUsers = faUsers;
  public faBell = faBell;
  constructor() { }

  ngOnInit(): void {
  }

}
