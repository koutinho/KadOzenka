import { FormControl, Validators } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';
import { SignUpResult } from '../common/guards/api/sign-up/sign-up-result';
import { SignUpApiService } from '../common/guards/api/sign-up/sign-up-service';
import { SignUpData } from '../common/guards/api/sign-up/sign-up-data';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  login: FormControl = new FormControl('' , Validators.required);
  password: FormControl = new FormControl('', Validators.required);
  email: FormControl = new FormControl('', [Validators.required, Validators.email]);

  signUpResult: SignUpResult | null = null;

  constructor(private signUpService: SignUpApiService) {
  }

  ngOnInit(): void {
  }

  signUp() {
    let signUpData = new SignUpData(this.login.value,
      this.email.value, this.password.value);

    this.signUpService.SignUp(signUpData)
      .subscribe((result) => {
        this.signUpResult = result;
        if (result.success)
          this.reset();
      }, () => {
        console.log  
      });
  }

  reset() {
    this.resetLogin();
    this.resetPassword();
    this.resetEmail();
  }

  resetLogin() {
    this.login.setValue('');
    this.login.markAsDirty();
    this.login.markAsUntouched();
  }

  resetPassword() {
    this.password.setValue('');
    this.password.markAsDirty();
    this.password.markAsUntouched();
  }

  resetEmail() {
    this.email.setValue('');
    this.email.markAsDirty();
    this.email.markAsUntouched();
  }
}
