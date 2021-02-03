import { OptionsInfoService } from './../../../services/options-info/options-info.service';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { AlertService } from 'src/app/services/alert/alert.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserAccount } from 'src/app/models/user-account/user-account';
import { ConnectService } from 'src/app/services/connect/connect.service';

@Component({
  selector: 'app-post-create',
  templateUrl: './post-create.component.html',
  styleUrls: ['./post-create.component.css']
})
export class PostCreateComponent implements OnInit {
  public postForm!: FormGroup;
  public submitted = false;
  public typeIncorrect = false;
  private selectedFile!: File;
  public userAccountSubscription!: any;
  public userData = new UserAccount();
  public isEmptySpace = false;
  public loading = false;
  
  constructor(private formBuilder: FormBuilder, 
              private connect: ConnectService, 
              private alertService: AlertService, 
              private router: Router, 
              private optionInfo: OptionsInfoService) { 
    this.userAccountSubscription = this.connect.userAccountData$.subscribe(value => {
      this.userData = value;
    });
    
    this.postFormBuilder();
  }

  ngOnInit(): void {
  }

  public onSubmit = () => {
    this.submitted = true;
    this.loading = true;
    if (this.postForm.invalid || this.postForm.controls.text.value.trim().length === 0) {
      this.isEmptySpace = true;
      this.loading = false;
      return;
    }
    if (this.typeIncorrect) {
      this.loading = false;
      return;
    }
    const formData = new FormData();
    formData.append('Name', this.userData.name);
    formData.append('PostText', this.postForm.controls.text.value);
    formData.append('Content', this.selectedFile);
    this.connect.postContentCreate(formData).pipe(tap(_ => {
      this.router.navigate([this.optionInfo.userProfilePath])
    })).subscribe();
  }

  private postFormBuilder = () => {
    this.postForm = this.formBuilder.group({
      text: [ null, Validators.required ]
    });
  }

  public get postFormControl() {
    return this.postForm.controls;
  }

  public fileOptions = (file: any) => {
    this.typeIncorrect = true;
    this.selectedFile = <File>file.target.files[0];
    if (this.selectedFile.type === 'image/png' || 
        this.selectedFile.type === 'image/jpeg'|| 
        this.selectedFile.type === 'video/mp3' || 
        this.selectedFile.type === 'video/mp4') {
      this.alertService.imageTypeValid();
      this.typeIncorrect = false;
    }
    else{
      this.alertService.imageTypeInvalid();
    }
  }

}
