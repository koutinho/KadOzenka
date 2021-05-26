import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './common/guards/auth.guard';
import { LoginComponent } from './login/login/login.component';
import { SettingComponent } from './setting/setting.component';

const routes: Routes = [
    {
      path: '',
      pathMatch: 'full',
      redirectTo: '/setting',
    },
    {
      path: 'setting',
      component: SettingComponent,
      canActivate: [AuthGuard]
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
