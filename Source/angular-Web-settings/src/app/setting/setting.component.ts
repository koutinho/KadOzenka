import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {ApiService} from '../common/api/api';
import {ControlModel} from "./model/control.model";
import {HttpErrorResponse} from "@angular/common/http";
import {ISetting} from "../common/api/Interface/setting.interface";
import {EnumControl} from "./enum/enum.control";

@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
  styleUrls: ['./setting.component.css']
})
export class SettingComponent implements OnInit {

  controls: ControlModel<any>[] | null = [new ControlModel<string[]>({
    key: "use",
    value: ["1 \r","2\r","3\r"],
    controlType: EnumControl.array,
    label: ""
  })];

  constructor(private api: ApiService, private route: Router) { }



  ngOnInit(): void {
    this.api.settingApi.getSetting()
      .subscribe((res: ISetting) => {

      }, (error: HttpErrorResponse) => {
      alert(error.error)
    });
  }

  logOut(){
    this.api.authApi.logOut().subscribe(() => {
      this.route.navigate(["signIn"]);
    })
  }
}
