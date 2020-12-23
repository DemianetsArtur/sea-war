import { faUserCircle } from '@fortawesome/free-solid-svg-icons';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ConnectService } from '../../../services/connect/connect.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { OptionsInfoService } from 'src/app/services/options-info/options-info.service';
import { catchError, tap } from 'rxjs/operators';
import { AlertService } from 'src/app/services/alert/alert.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  public loading = false;
  public registerForm!: FormGroup;
  public submitted = false;
  public returnUrl!: string;
  public equalPassword = false;
  public file!: FormData ;
  public fileEmpty = false;
  public typeIncorrect = false;
  public faUserCircle = faUserCircle;

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
    debugger;
    this.submitted = true;
    if (this.registerForm.invalid) {
      return;
    }
    if (this.registerForm.controls.confirmPassword.value !== this.registerForm.controls.password.value) {
      this.equalPassword = true;
      return;
    }
    if (this.fileEmpty || this.typeIncorrect){
      return;
    }
    if (this.file === undefined){
      this.fileEmpty = true;
      return;
    }
    this.loading = true;
    const returnUrl = this.route.snapshot.queryParamMap.get(this.optionsInfo.returnUrl) || '/';
    this.connect.registerPost(this.registerForm.value)
                .pipe(tap(data => {
                   
                  this.connect.imagePost(this.file, this.registerForm.controls.name.value);
                  this.router.navigate([returnUrl]);
                  this.router.navigate(['/login'],{queryParams: {brandNew: true,email:this.registerForm.controls.email.value}});
                }),
                catchError(async (err) => {
                  this.alertService.sameUserAlert();
                  this.loading = false;
                  this.registerForm.reset();
                  this.registerForm.setErrors({ invalidLogin: true });
                }))
                .subscribe();          
  }

  public handlerTypeImage = (files: any) => {
    debugger;
    this.typeIncorrect = false;
    this.fileEmpty = false;
    // if (files[0].type !== 'image/png') {
    //   this.typeIncorrect = true;
    // }
    if (files.length === 0) {
      this.fileEmpty = true;
    }
    if (files[0].type !== 'image/jpeg') {
      this.typeIncorrect = true;
    }
  }

  public setFileOption = (files: any) => {
    this.handlerTypeImage(files);
    this.file = files;
  }
  
  public get registerFormControl() {
    return this.registerForm.controls;
  }

  private handlerFormBuilder = () => {
    this.registerForm = this.formBuilder.group({
      name: [null, Validators.required],
      password: [null, Validators.compose([
        Validators.required,
        Validators.minLength(8)
      ])],
      firstName: [null, Validators.required],
      lastName: [null, Validators.required],
      email: [null, Validators.compose([
        Validators.required,
        Validators.email
      ])],
      aboutMe: [null, Validators.required],
      confirmPassword: [null, Validators.required],
      date: [ null, Validators.required ]
    });
  }
}
