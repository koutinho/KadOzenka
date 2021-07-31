import { Component, OnInit } from '@angular/core';
import { SignUpResult } from './Data/sign-up-result';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  login = '';
  password = '';
  email = '';
  signUpResult: SignUpResult | null = null;

  constructor() { }

  ngOnInit(): void {
  }

  signUp() {
    this.signUpResult = new SignUpResult(false, "Ошибка при регистраци аккаунта.");
    this.reset();
  }

  reset() {
    this.login = '';
    this.password = '';
    this.email = '';
  }
}
