import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Http } from "../httpMethods/methods";
import { SignUpData } from "./sign-up-data";
import { SignUpResult } from "./sign-up-result";

@Injectable()
export class SignUpApiService {
    private http: Http;

    constructor(httpClient: HttpClient){
        this.http = new Http(httpClient);
    }

    SignUp(signUpData: SignUpData): SignUpResult {
        return new SignUpResult(false, "Ошибка при регистраци аккаунта.");
    }
}