import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../common/api/api';

@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
  styleUrls: ['./setting.component.css']
})
export class SettingComponent implements OnInit {

  constructor(private api: ApiService, private route: Router) { }



  ngOnInit(): void {
    this.api.settingApi.getSetting().subscribe(d => console.log(d));
  }

  logOut(){
    this.api.authApi.logOut().subscribe(res => {
      this.route.navigate(["signIn"]);
    })
  }
}
