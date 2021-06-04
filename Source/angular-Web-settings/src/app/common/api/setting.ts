import {HttpClient} from "@angular/common/http";
import {Http} from "./httpMethods/methods";
import {Observable} from "rxjs";
import {ISetting} from "./Interface/setting.interface";

export class SettingApi{

  private readonly http: Http;
  constructor(httpClient: HttpClient) {
    this.http = new Http(httpClient);
  }

  getSettingSerilog(): Observable<ISetting>{
    return this.http.get<ISetting>("api/Config/GetConfigurations")
  }

  setSettingSerilog(data: ISetting): Observable<boolean>{
    return this.http.post("api/Config/SetConfigurations", data)
  }
}
