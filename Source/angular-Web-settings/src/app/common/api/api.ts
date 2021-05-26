import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AuthApiService } from "./auth";

@Injectable()
export class ApiService {

    private _authApi: AuthApiService;
    get authApi(): AuthApiService{
        return this._authApi;
    }

    constructor(private httpClient: HttpClient){
        this._authApi = new AuthApiService(httpClient);
    }

}