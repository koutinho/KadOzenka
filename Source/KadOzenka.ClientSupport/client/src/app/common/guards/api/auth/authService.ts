import { HttpClient, HttpHandler, HttpHeaders, HttpResponse } from "@angular/common/http"
import { from, Observable, of } from "rxjs";
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
        console.log('Trying to authorize ' + model.userName);
        return of(true);
        //return this.http.post<LoginData, boolean>("authenticate/login", model)
    }
}