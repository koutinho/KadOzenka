import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './common/guards/auth.guard';
import { LoginComponent } from './login/login/login.component';
import { SettingComponent } from './setting/setting.component';
import {EnumRouteSetting} from "./common/route/enum.route";

const routes: Routes = [
    {
      path: '',
      pathMatch: 'full',
      redirectTo: '/setting/serilog',
    },
  {
    path: 'setting',
    pathMatch: 'full',
    redirectTo: '/setting/serilog',
  },
    {
      path: 'setting/serilog',
      component: SettingComponent,
      canActivate: [AuthGuard],
      data: {
        setting: EnumRouteSetting.serilog
      }
    },
  {
    path: 'setting/ko',
    component: SettingComponent,
    canActivate: [AuthGuard],
    data: {
      setting: EnumRouteSetting.ko
    }
  },
  {
    path: 'setting/core',
    component: SettingComponent,
    canActivate: [AuthGuard],
    data: {
      setting: EnumRouteSetting.core
    }
  },
    {
      path: 'signIn',
      component: LoginComponent
    },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuard]
})
export class AppRoutingModule { }
