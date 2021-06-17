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
    return this.http.get<ISetting>("api/Config/GetSerilogConfigurations")
  }

  getSettingCore(): Observable<ISetting>{
    return this.http.get<ISetting>("api/Config/GetCoreConfigurations")
  }

  getSettingKo(): Observable<ISetting>{
    return this.http.get<ISetting>("api/Config/GetKoConfigurations")
  }

  setSettingSerilog(data: ISetting): Observable<boolean>{
    return this.http.post("api/Config/SetSerilogConfigurations", data)
  }

  setSettingCore(data: ISetting): Observable<boolean>{
    return this.http.post("api/Config/SetCoreConfigurations", data)
  }

  setSettingKo(data: ISetting): Observable<boolean>{
    return this.http.post("api/Config/SetKoConfigurations", data)
  }
}
