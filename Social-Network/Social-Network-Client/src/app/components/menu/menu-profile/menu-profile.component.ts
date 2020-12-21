import { Component, OnInit } from '@angular/core';
import { faUser, faPencilAlt, faComment, faUsers, faBell, faHome } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-menu-profile',
  templateUrl: './menu-profile.component.html',
  styleUrls: ['./menu-profile.component.css']
})
export class MenuProfileComponent implements OnInit {
  public faUser = faUser;
  public faPencilAlt = faPencilAlt;
  public faComment = faComment;
  public faUsers = faUsers;
  public faBell = faBell;
  public faHome = faHome;
  constructor() { }

  ngOnInit(): void {
  }

}
