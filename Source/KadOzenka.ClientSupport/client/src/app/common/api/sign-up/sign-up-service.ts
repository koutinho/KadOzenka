import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of, throwError } from "rxjs";
import { catchError, map } from "rxjs/operators";
import { Http } from "../httpMethods/methods";
import { SignUpData } from "./sign-up-data";
import { SignUpResponse } from "./sign-up-response";
import { SignUpResult } from "./sign-up-result";

@Injectable()
export class SignUpApiService {
    private http: Http;

    constructor(httpClient: HttpClient){
        this.http = new Http(httpClient);
    }

    SignUp(signUpData: SignUpData): Observable<SignUpResult> {
        return this.http.post<SignUpData, SignUpResponse>("authenticate/register", signUpData)
          .pipe(
            map(res => new SignUpResult(true, res.message)),
            catchError(this.handleError));        
    }

    private handleError(error: HttpErrorResponse) {
      console.error('Ошибка:', error.error);

      if (error.status === 500) {
        return of<SignUpResult>(new SignUpResult(false, error.error.message));
      } else {
        return of<SignUpResult>(new SignUpResult(false, "Возникла непредвиденная ошибка"))
      }
    }
}