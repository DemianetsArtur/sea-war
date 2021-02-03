import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/user-account/login/login.component';
import { UserHomeComponent } from './components/user-account/user-home/user-home.component';
import { AdminHomeComponent } from './components/user-account/admin-home/admin-home.component';
import { NavMenuComponent } from './components/menu/nav-menu/nav-menu.component';
import { HttpInterceptorService } from './services/http-interceptor/http-interceptor.service';
import { ErrorInterceptorService } from './services/error-interceptor/error-interceptor.service';
import { RegisterComponent } from './components/user-account/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MenuProfileComponent } from './components/menu/menu-profile/menu-profile.component';
import { FriendListComponent } from './components/friend/friend-list/friend-list.component';
import { EditComponent } from './components/edit/edit.component';
import { NotificationComponent } from './components/notification/notification.component';
import { NavigateMenuComponent } from './components/menu/navigate-menu/navigate-menu.component';
import { MessagesComponent } from './components/messages/messages.component';
import { MaterialDesignModule } from './modules/material-design/material-design.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { UsersProfileComponent } from './components/users-profile/users-profile.component';
import { PostCreateComponent } from './components/post/post-create/post-create.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    UserHomeComponent,
    AdminHomeComponent,
    NavMenuComponent,
    RegisterComponent,
    HomeComponent,
    UserProfileComponent,
    MenuProfileComponent,
    FriendListComponent,
    EditComponent,
    NotificationComponent,
    NavigateMenuComponent,
    MessagesComponent,
    UsersProfileComponent,
    PostCreateComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    FontAwesomeModule,
    MaterialDesignModule,
    FlexLayoutModule,
    MatInputModule,
    MatFormFieldModule
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: HttpInterceptorService, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptorService, multi: true },
    ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  exports: [
    MatInputModule,
    MatFormFieldModule
  ]
})
export class AppModule { }
