import { Injectable } from "@angular/core";
import { HttpClient, HttpHandler, HttpHeaders, HttpResponse } from "@angular/common/http"
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { Http } from "./httpMethods/methods";

export class AuthApiService {
    private http: Http;
    constructor(httpClient: HttpClient){
        this.http = new Http(httpClient);
    }
    
    checkAuth(): Observable<boolean>{
        return this.http.get<boolean>("api/Auth/CheckAuthorize")
    }
}