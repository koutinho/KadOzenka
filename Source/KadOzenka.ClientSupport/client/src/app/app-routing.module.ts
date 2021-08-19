import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './common/guards/auth.guard';
import { JwtModule } from '@auth0/angular-jwt';
import { Settings } from 'src/settings';
import { SignUpComponent } from './sign-up/sign-up.component';
import { TicketsComponent } from './tickets/tickets.component';
import { AddTicketComponent } from './add-ticket/add-ticket.component';
import { LoginLayoutComponent } from './layouts/login-layout.component';
import { HomeLayoutComponent } from './layouts/home-layout.component';

export function tokenGetter() {
  return localStorage.getItem("jwt");
}

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: '/home',
  },
  {
    path: '',
    component: HomeLayoutComponent,
    children: [
      {
        path: 'home',
        canActivate: [AuthGuard],
        component: HomeComponent
      },
      {
        path: 'tickets',
        canActivate: [AuthGuard],
        component: TicketsComponent
      },
      {
        path: 'tickets/add',
        canActivate: [AuthGuard],
        component: AddTicketComponent
      }
    ]
  },
  {
    path: '',
    component: LoginLayoutComponent,
    children: [
      {
        path: 'login',
        component: LoginComponent
      },
      {
        path: 'signup',
        component: SignUpComponent
      }
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: [Settings.apiDomain],
        disallowedRoutes: []
      }
    })
  ],
  exports: [RouterModule],
  providers: [AuthGuard]
})
export class AppRoutingModule { }
