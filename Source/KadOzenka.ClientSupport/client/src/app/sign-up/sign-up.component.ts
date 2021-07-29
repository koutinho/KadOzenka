import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  login = '';
  password = '';
  email = '';

  signUpSuccess = false;
  signUpError = false;
  signUpErrorMessage = "Ошибка при регистраци аккаунта."

  constructor() { }

  ngOnInit(): void {
  }

  signUp() {
    this.signUpError = true;
  }
}
