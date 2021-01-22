import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, first, tap } from 'rxjs/operators';
import { ConnectService } from '../../../services/connect/connect.service';
import { OptionsInfoService } from '../../../services/options-info/options-info.service';
import { AlertService } from '../../../services/alert/alert.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public loading = false;
  public loginForm!: FormGroup;
  public submitted = false;
  public returnUrl!: string;
  constructor(private formBuilder: FormBuilder, 
              private route: ActivatedRoute,    
              private router: Router,
              private connect: ConnectService, 
              private optionsInfo: OptionsInfoService, 
              private alertService: AlertService) { }

  ngOnInit(): void {
    this.handlerFormBuilder();
  }

  public onSubmit = () => {
    this.submitted = true;
    if (this.loginForm.invalid) {
      return;
    }
    this.loading = true;
    const returnUrl = this.route.snapshot.queryParamMap.get(this.optionsInfo.returnUrl) || '/';
    this.connect.loginPost(this.loginForm.value)
                .pipe(tap(data => {
                    this.router.navigate([returnUrl]);
                }),
                catchError(async (err) => {
                  
                  if (err === 401){
                    console.log('err:', err)
                    this.alertService.userNotExist();
                    this.loading = false;
                    this.loginForm.reset();
                    this.loginForm.setErrors({ invalidLogin: true });
                  }
                  else if (err === 402){
                    console.log('err:', err)
                    this.alertService.expiredToken();
                    this.loading = false;
                    this.loginForm.reset();
                    this.loginForm.setErrors({ invalidLogin: true });
                  }
                  else if (err === 403){
                    console.log('err:', err)
                    this.alertService.userNotConfirmEmail();
                    this.loading = false;
                    this.loginForm.reset();
                    this.loginForm.setErrors({ invalidLogin: true });
                  }
                  
                }))
                .subscribe();             
  }

  public get loginFormControl() {
    return this.loginForm.controls;
  }

  private handlerFormBuilder = () => {
    this.loginForm = this.formBuilder.group({
      name: [null, Validators.required],
      password: [null, Validators.compose([
        Validators.required,
        Validators.minLength(8)
      ])]
    });
  }
}
