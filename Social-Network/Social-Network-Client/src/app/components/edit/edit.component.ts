import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { faHouseUser } from '@fortawesome/free-solid-svg-icons';
import { ConnectService } from 'src/app/services/connect/connect.service';
import { OptionsInfoService } from 'src/app/services/options-info/options-info.service';
import { AlertService } from 'src/app/services/alert/alert.service';
import { tap, catchError } from 'rxjs/operators';
import { UserAccount } from 'src/app/models/user-account/user-account';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  public loading = false;
  public editForm!: FormGroup;
  public submitted = false;
  public returnUrl!: string;
  public equalPassword = false;
  public file!: FormData ;
  public isDateGreater = false;
  public fileEmpty = false;
  public typeIncorrect = false;
  public faUsers = faHouseUser;
  public userAccountSubscription!: any;
  public userData = new UserAccount();
  public userAccountCurrentSubscription!: any;
  public userAccountCurrentData = new UserAccount();
  constructor(private formBuilder: FormBuilder, 
              private route: ActivatedRoute,    
              private router: Router,
              private connect: ConnectService, 
              private optionsInfo: OptionsInfoService, 
              private alertService: AlertService) {
    this.userAccountSubscription = this.connect.userAccountData$.subscribe(value => {
      this.userData = value;
    });    
    this.userAccountCurrentSubscription = this.connect.userGet(this.userData.name);
    this.userAccountCurrentSubscription = this.connect.userAccountCurrentValue$.subscribe(value => {
      this.userAccountCurrentData = value;
    });   
    this.handlerFormBuilder();
    this.handlerAboutMeSetValue();
    this.handlerDataEdit(); 
  }

  ngOnInit(): void {

    
  }

  public handlerAboutMeSetValue = () => {
    this.editForm.controls.aboutMe.setValue(this.userAccountCurrentData.aboutMe);
  }

  private handlerDataEdit = () => {
    const name = this.editForm.controls.name.value;
    const firstName = this.editForm.controls.firstName.value;
    const lastName = this.editForm.controls.lastName.value;
    const email = this.editForm.controls.email.value;
    const aboutMe = this.editForm.controls.aboutMe.value;
    const date = this.editForm.controls.date.value;
    if (name === undefined || name === null){
      this.editForm.controls.name.setValue(this.userAccountCurrentData.name);
    }  
    if (firstName === undefined || firstName === null){
      this.editForm.controls.firstName.setValue(this.userAccountCurrentData.firstName);
    }
    if (lastName === undefined || lastName === null){
      this.editForm.controls.lastName.setValue(this.userAccountCurrentData.lastName);
    }
    if (email === undefined || email === null){
      this.editForm.controls.email.setValue(this.userAccountCurrentData.email);
    }
    if (aboutMe === undefined || aboutMe === null){
      this.editForm.controls.aboutMe.setValue(this.userAccountCurrentData.aboutMe);
    }
    if (date === undefined || date === null){
      this.editForm.controls.date.setValue(this.userAccountCurrentData.date);
    }
    if(this.file === undefined || this.file === null){
      this.editForm.controls.imagePath.setValue(this.userAccountCurrentData.imagePath);
    }
  }

  public onSubmit = () => {
    this.submitted = true;
    
    if (this.editForm.invalid) {
      this.handlerDataEdit();
      return;
    }
    if (this.typeIncorrect){
      return;
    }
    this.handlerDateValidation(this.editForm.controls.date.value);

    if (this.isDateGreater){
      return;
    }

    this.loading = true;
    const returnUrl = this.route.snapshot.queryParamMap.get(this.optionsInfo.returnUrl) || '/';
    this.connect.userEditPost(this.editForm.value, this.userAccountCurrentData)
                .pipe(tap(data => {
                  window.location.reload();
                }),
                catchError(async (err) => {
                  this.alertService.sameUserAlert();
                  this.loading = false;
                  this.editForm.reset();
                  this.editForm.setErrors({ invalidLogin: true });
                }))
                .subscribe(); 
    if (this.file !== undefined){
      this.connect.imagePost(this.file, this.userAccountCurrentData.name );
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

  public get editFormControl() {
    return this.editForm.controls;
  }

  public setFileOption = (files: any) => {
    this.handlerTypeImage(files);
    this.file = files;
  }

  public handlerTypeImage = (files: any) => {
    this.typeIncorrect = true;
    if (files[0].type === 'image/png' || files[0].type === 'image/jpeg') {
      this.alertService.imageChange();
      this.typeIncorrect = false;
    }
  }

  private handlerFormBuilder = () => {
    this.editForm = this.formBuilder.group({
      name: [null],
      firstName: [null ],
      lastName: [null ],
      email: [null, Validators.email ],
      aboutMe: [null ],
      date: [ null],
      imagePath: [ null ]
    });
  }
}
