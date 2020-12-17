import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/user-account/login/login.component';
import { UserHomeComponent } from './components/user-account/user-home/user-home.component';
import { UserGuard } from './guards/user-account/user/user.guard';
import { AdminHomeComponent } from './components/user-account/admin-home/admin-home.component';
import { AdminGuard } from './guards/user-account/admin/admin.guard';
import { RegisterComponent } from './components/user-account/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'admin-home', component: AdminHomeComponent, canActivate: [AdminGuard] },
  { path: 'user-profile', component: UserProfileComponent, canActivate: [UserGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
