import { Component, OnInit } from '@angular/core';
import { faUser, faPencilAlt, faComment, faUsers, faBell, faHome } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-navigate-menu',
  templateUrl: './navigate-menu.component.html',
  styleUrls: ['./navigate-menu.component.css']
})
export class NavigateMenuComponent implements OnInit {
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
