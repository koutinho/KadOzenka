import {Injectable} from "@angular/core";
import {ISetting} from "../../common/api/Interface/setting.interface";
import {EnumRouteSetting} from "../../common/route/enum.route";
import {Observable, of} from "rxjs";
import {ApiService} from "../../common/api/api";

@Injectable()
export class SettingService {

  constructor(private api: ApiService) {
  }
  getSetting(settingType: EnumRouteSetting): Observable<ISetting> {
    const res : ISetting = {
      RootConfig: "",
      EnvConfig: ""
    };

    switch (settingType){
      case EnumRouteSetting.serilog: return this.api.settingApi.getSettingSerilog()
    }


    return of(res);
  }

  setSetting(data: ISetting): Observable<boolean>{

  }


}
