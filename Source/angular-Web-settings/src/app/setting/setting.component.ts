import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {Router} from '@angular/router';
import {ApiService} from '../common/api/api';
import {ControlModel} from "./model/control.model";
import {HttpErrorResponse} from "@angular/common/http";
import {ISetting} from "../common/api/Interface/setting.interface";
import JSONFormatter from 'json-formatter-js';
import JSONEditor from "jsoneditor";

@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
  styleUrls: ['./setting.component.css']
})
export class SettingComponent implements OnInit {

  controls: ControlModel<any>[] | null = [];
  @ViewChild('jsonEditorEnv') jsonEditorEnv!:ElementRef;

  @ViewChild('jsonEditorRoot') jsonEditorRoot!: ElementRef;

  constructor(private api: ApiService, private route: Router) { }



  ngOnInit(): void {
    this.api.settingApi.getSetting()
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
    const editor = new JSONEditor(container, options)
    editor.set(rootConfig);


    const containerEnv: HTMLElement = this.jsonEditorEnv.nativeElement;
    const editorEnv = new JSONEditor(containerEnv, options)
    editorEnv.set(rootConfig);
  }

}
