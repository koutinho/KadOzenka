import { FormControl, Validators, FormBuilder, FormGroup, AbstractControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { SignUpResult } from '../common/api/sign-up/sign-up-result';
import { SignUpApiService } from '../common/api/sign-up/sign-up-service';
import { SignUpData } from '../common/api/sign-up/sign-up-data';
import { MustMatchValidator } from '../common/validators/MustMatchValidator';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  signUpForm: FormGroup = this.formBuilder.group({
    login: ['', Validators.required],
    password: ['', Validators.required],
    confirmPassword: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]]
  }, {
    validator: MustMatchValidator('password', 'confirmPassword')
  });

  signUpResult: SignUpResult | null = null;

  constructor(private signUpService: SignUpApiService, private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
  }

  get login(): AbstractControl {
    return this.signUpForm.get('login') as AbstractControl;
  }

  get password(): AbstractControl {
    return this.signUpForm.get('password') as AbstractControl;
  }

  get confirmPassword(): AbstractControl {
    return this.signUpForm.get('confirmPassword') as AbstractControl;
  }

  get email(): AbstractControl {
    return this.signUpForm.get('email') as AbstractControl;
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

  resetConfirmPassword() {
    this.confirmPassword.setValue('');
    this.confirmPassword.markAsDirty();
    this.confirmPassword.markAsUntouched();
  }

  resetEmail() {
    this.email.setValue('');
    this.email.markAsDirty();
    this.email.markAsUntouched();
  }
}
