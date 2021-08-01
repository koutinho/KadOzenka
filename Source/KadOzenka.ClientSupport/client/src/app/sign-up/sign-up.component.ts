import { FormControl, Validators } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';
import { SignUpResult } from './Data/sign-up-result';

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

  constructor() { }

  ngOnInit(): void {
  }

  signUp() {
    this.signUpResult = new SignUpResult(false, "Ошибка при регистраци аккаунта.");
    this.reset();
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
