import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
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

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    UserHomeComponent,
    AdminHomeComponent,
    NavMenuComponent,
    RegisterComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: HttpInterceptorService, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptorService, multi: true },
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
