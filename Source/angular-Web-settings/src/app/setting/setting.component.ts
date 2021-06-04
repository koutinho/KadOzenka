import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {ApiService} from '../common/api/api';
import {ControlModel} from "./model/control.model";
import {HttpErrorResponse} from "@angular/common/http";
import {ISetting} from "../common/api/Interface/setting.interface";
import JSONEditor from "jsoneditor";
import {EnumRouteSetting} from "../common/route/enum.route";
import {SettingService} from "./service/setting.service";

@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
  styleUrls: ['./setting.component.css'],
  providers: [SettingService]
})
export class SettingComponent implements OnInit {

  showLoader: boolean = false;
  rootEditor!: JSONEditor;
  envEditor!: JSONEditor;

  @ViewChild('jsonEditorEnv') jsonEditorEnv!:ElementRef;

  @ViewChild('jsonEditorRoot') jsonEditorRoot!: ElementRef;

  constructor(private api: ApiService,
              private route: Router,
              private activatedRoute: ActivatedRoute,
              private settingService: SettingService) { }



  ngOnInit(): void {
    this.settingService.getSetting(this.activatedRoute.snapshot.data["setting"])
      .subscribe((res: ISetting) => {
        this.settingBindToObject(res);
      }, (error: HttpErrorResponse) => {
      alert(error.error)
    });
  }

  logOut(){
    this.api.authApi.logOut().subscribe(() => {
      this.route.navigate(["signIn"]);
    })
  }

  settingBindToObject(data: ISetting){
    const rootConfig = data.RootConfig && JSON.parse(data.RootConfig);
    const envConfig = data.EnvConfig && JSON.parse(data.EnvConfig);

    const container: HTMLElement = this.jsonEditorRoot.nativeElement;
    const options = {}
    this.rootEditor = new JSONEditor(container, options)
    this.rootEditor.set(rootConfig);


    const containerEnv: HTMLElement = this.jsonEditorEnv.nativeElement;
    this.envEditor = new JSONEditor(containerEnv, options)
    this.envEditor.set(envConfig);
  }

  saveSetting(){
    this.showLoader = true;
    const data: ISetting  = {
      RootConfig: JSON.stringify(this.rootEditor.get()),
      EnvConfig: JSON.stringify(this.envEditor.get())
    }

    this.api.settingApi.setSetting(data).subscribe((res) => {
      this.showLoader = false;
      alert("Успешно сохранено")
    }, (error: HttpErrorResponse) => {
      this.showLoader = false;
      alert(error.error)
    })

  }

}
