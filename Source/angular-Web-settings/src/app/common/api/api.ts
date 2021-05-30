import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AuthApiService } from "./auth";
import {SettingApi} from "./setting";

@Injectable()
export class ApiService {

    private readonly _authApi: AuthApiService;
    get authApi(): AuthApiService{
        return this._authApi;
    }
    private readonly _settingApi: SettingApi;
    get settingApi(): SettingApi{
      return this._settingApi;
    }

    constructor(private httpClient: HttpClient){
        this._authApi = new AuthApiService(httpClient);
        this._settingApi = new SettingApi(httpClient);
    }

}
