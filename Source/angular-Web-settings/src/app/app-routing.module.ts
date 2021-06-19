import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './common/guards/auth.guard';
import { LoginComponent } from './login/login.component';
import { SettingComponent } from './setting/setting.component';
import {EnumSection} from "./common/route/enum.route";

const routes: Routes = [
    {
      path: '',
      pathMatch: 'full',
      redirectTo: '/setting',
    },
    {
      path: 'setting',
      component: SettingComponent,
      canActivate: [AuthGuard],
      data: {
        setting: EnumSection.serilog
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
