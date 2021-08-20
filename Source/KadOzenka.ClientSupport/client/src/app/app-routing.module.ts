import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './common/guards/auth.guard';
import { JwtModule } from '@auth0/angular-jwt';
import { Settings } from 'src/settings';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { TicketsComponent } from './components/tickets/tickets.component';
import { AddTicketComponent } from './components/add-ticket/add-ticket.component';
import { LoginLayoutComponent } from './components/layouts/login-layout/login-layout.component';
import { HomeLayoutComponent } from './components/layouts/home-layout/home-layout.component';

export function tokenGetter() {
  return localStorage.getItem("jwt");
}

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: '/tickets',
  },
  {
    path: '',
    component: HomeLayoutComponent,
    children: [
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
