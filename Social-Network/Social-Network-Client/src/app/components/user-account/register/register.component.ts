import { faUserCircle } from '@fortawesome/free-solid-svg-icons';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ConnectService } from '../../../services/connect/connect.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { OptionsInfoService } from 'src/app/services/options-info/options-info.service';
import { catchError, tap } from 'rxjs/operators';
import { AlertService } from 'src/app/services/alert/alert.service';
import { UserRole } from 'src/app/models/user-account/user-role';
import { ErrorStateMatcher } from '@angular/material/core';


export class FormErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

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
  public isDateGreater = false;
  public selectedFile!: File;
  public matcher = new FormErrorStateMatcher();

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
    this.handlerDateValidation(this.registerForm.controls.date.value);
    if (this.registerForm.invalid || this.typeIncorrect || this.selectedFile === undefined || this.isDateGreater) {
      return;
    }

    if (this.registerForm.controls.confirmPassword.value !== this.registerForm.controls.password.value) {
      this.equalPassword = true;
      return;
    }

    

    this.loading = true;
    const returnUrl = this.route.snapshot.queryParamMap.get(this.optionsInfo.returnUrl) || '/';

    this.connect.registerPost(this.handlerUserAccountForm())
                .pipe(tap(_ => {
                  this.alertService.userConfirmEmail(this.registerForm.controls.email.value); 
                  this.router.navigate(['/login'],{queryParams: {nickname: this.registerForm.controls.name.value}});
                }),
                            catchError(async (err) => {
                              if (err === 401) {
                                this.handlerErrorStatus();
                              }
                              if(err === 402){
                                this.alertService.sameUserAlert();
                                this.handlerErrorStatus();
                              }
                            }))
                .subscribe();       
  }

  private handlerErrorStatus = () => {
    this.loading = false;
    this.registerForm.reset();
    this.registerForm.setErrors({ invalidLogin: true });
  }

  private handlerUserAccountForm = () => {
    const formData = new FormData();
    formData.append('Name', this.registerForm.controls.name.value);
    formData.append('Password', this.registerForm.controls.password.value);
    formData.append('FirstName', this.registerForm.controls.firstName.value);
    formData.append('LastName', this.registerForm.controls.lastName.value);
    formData.append('Email', this.registerForm.controls.email.value);
    formData.append('AboutMe', this.registerForm.controls.aboutMe.value);
    formData.append('Date', this.registerForm.controls.date.value);
    formData.append('Content', this.selectedFile);
    formData.append('UserType', UserRole.User);
    debugger;
    return formData;
  }

  public handlerTypeImage = (files: any) => {
    this.typeIncorrect = true;
    if (files[0].type === 'image/png' || files[0].type === 'image/jpeg') {
      this.alertService.imageTypeValid();
      this.typeIncorrect = false;
      this.fileEmpty = false;
    }
    else{
      this.alertService.imageTypeInvalid();
    }
  }

  public fileOptions = (file: any) => {
    this.typeIncorrect = true;
    this.selectedFile = <File>file.target.files[0];
    if (this.selectedFile.type === 'image/png' || 
        this.selectedFile.type === 'image/jpeg') {
      this.alertService.imageTypeValid();
      this.typeIncorrect = false;
    }
    else{
      this.alertService.imageTypeInvalid();
    }
  }

  public handlerDateValidation = (date: any) => {
    const dateNow = new Date();
    const dateLessInfo = dateNow.getFullYear() - 140;
    const dateLess = new Date(dateLessInfo);

    const givenData = new Date(date);
    this.isDateGreater = false;
    if (givenData > dateNow || givenData < dateLess){
      this.isDateGreater = true;
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
      confirmPassword: [null, Validators.compose([
        Validators.required,
        Validators.minLength(8)
      ])],
      date: [ null, Validators.required ]
    });
  }
}
