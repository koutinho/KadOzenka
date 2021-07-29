import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  login = '';
  password = ''

  signUpSuccess = false;

  constructor() { }

  ngOnInit(): void {
  }

  signUp() {
    this.signUpSuccess = true;
  }
}
