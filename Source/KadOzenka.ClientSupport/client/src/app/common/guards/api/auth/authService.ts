import { HttpClient, HttpHandler, HttpHeaders, HttpResponse } from "@angular/common/http"
import { from, Observable, of } from "rxjs";
import { tap, map, catchError } from "rxjs/operators";
import { Http } from "../httpMethods/methods";
import { LoginData } from "./data/loginData";
import { Injectable } from "@angular/core";

@Injectable()
export class AuthApiService {
    private http: Http;
    constructor(httpClient: HttpClient){
        this.http = new Http(httpClient);
    }

    logIn(model: LoginData): Observable<boolean>{
        return this.http.post<LoginData, any>("authenticate/login", model)
          .pipe(
            tap((res) => {
              localStorage.setItem("jwt", res.token);
            }),
            map(res => true),
            catchError(err => {
              if (err.status == 401) {
                return of(false)
              }
              throw err;
            })
          );
    }
}