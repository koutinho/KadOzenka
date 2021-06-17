import {Component, ElementRef, EventEmitter, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {ApiService} from '../common/api/api';
import {HttpErrorResponse} from "@angular/common/http";
import {ISetting} from "../common/api/Interface/setting.interface";
import JSONEditor from "jsoneditor";
import {SettingService} from "./service/setting.service";
import {MatSelectChange} from "@angular/material/select";
import {EnumSection} from "../common/route/enum.route";

@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
  styleUrls: ['./setting.component.css'],
  providers: [SettingService]
})
export class SettingComponent implements OnInit {

  selectedSection: EnumSection = EnumSection.serilog;
  title = "Секция серилог";
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
  this.loadSection(this.selectedSection);
  }

  private loadSection(section: EnumSection){
    this.settingService.getSetting(section)
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
    container.innerText = "";
    const options = {}
    this.rootEditor = new JSONEditor(container, options)
    this.rootEditor.set(rootConfig);


    const containerEnv: HTMLElement = this.jsonEditorEnv.nativeElement;
    containerEnv.innerText = "";
    this.envEditor = new JSONEditor(containerEnv, options)
    this.envEditor.set(envConfig);
  }

  saveSetting(){
    this.showLoader = true;
    const data: ISetting  = {
      RootConfig: JSON.stringify(this.rootEditor.get()),
      EnvConfig: JSON.stringify(this.envEditor.get())
    }

    this.settingService.setSetting(data, this.selectedSection).subscribe((res) => {
      this.showLoader = false;
      alert("Успешно сохранено")
    }, (error: HttpErrorResponse) => {
      this.showLoader = false;
      alert(error.error)
    })

  }

  selectionChange(event: MatSelectChange){
    const { value } = event;
    if(value){
      this.loadSection(value)
      switch (value){
        case EnumSection.serilog: this.title = "Секция серилог"; break;
        case EnumSection.core: this.title = "Секция платформы"; break;
        case EnumSection.ko: this.title = "Секция Кад. Оценки"; break;
      }
    }
  }

}
