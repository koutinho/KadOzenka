import {Injectable} from "@angular/core";
import {ISetting} from "../../common/api/Interface/setting.interface";
import {EnumSection} from "../../common/route/enum.route";
import {Observable, of} from "rxjs";
import {ApiService} from "../../common/api/api";

@Injectable()
export class SettingService {

  constructor(private api: ApiService) {
  }
  getSetting(settingType: EnumSection): Observable<ISetting> {
    const res : ISetting = {
      RootConfig: "",
      EnvConfig: ""
    };

    switch (settingType){
      case EnumSection.serilog: return this.api.settingApi.getSettingSerilog()
      case EnumSection.core: return this.api.settingApi.getSettingCore()
      case EnumSection.ko: return this.api.settingApi.getSettingKo()
    }


    return of(res);
  }

  setSetting(data: ISetting, settingType: EnumSection): Observable<boolean>{
    switch (settingType){
      case EnumSection.serilog: return this.api.settingApi.setSettingSerilog(data);
      case EnumSection.core: return this.api.settingApi.setSettingCore(data);
      case EnumSection.ko: return this.api.settingApi.setSettingKo(data);
    }

    return of(false)
  }


}
