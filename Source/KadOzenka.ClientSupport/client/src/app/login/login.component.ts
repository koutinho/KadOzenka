import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthApiService } from '../common/api/auth/authService';
import { LoginData } from '../common/api/auth/data/loginData';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  login = '';
  password = ''

  constructor(private api: AuthApiService, private router: Router) { }

  ngOnInit(): void {
  }

  signIn(){
    let loginData = new LoginData(this.login, this.password);

    this.api.logIn(loginData).subscribe((res) => {
        if (res) {
          this.router.navigate(["tickets"]);
        } else {
          console.log("Неправильный логин или пароль")
        }
      }, (err) =>
      {
        console.log
      });
  }
}
