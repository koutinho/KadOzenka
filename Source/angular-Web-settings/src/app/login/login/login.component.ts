import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ApiService } from 'src/app/common/api/api';

@Component({
  selector: 'ko-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  login = '';
  password = ''

  constructor(private api: ApiService, private router: Router) { }

  ngOnInit(): void {
  }

  signIn(){
    this.api.authApi.signIn({
      userName: this.login,
      password: this.password
    }).pipe(
    catchError((errorResponse: HttpErrorResponse) => {
      return throwError(errorResponse.error)
    })).subscribe(() => {
      this.router.navigate(["setting"]);
    }, errMsg => {
      console.log()
      alert(errMsg);
    });
  }
}
