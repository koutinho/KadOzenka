import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatDividerModule } from '@angular/material/divider';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AuthApiService } from './common/api/auth/authService';
import { WheatherForecastApiService } from './common/api/wheatherForcast/wheatherForecastService';
import { SignUpComponent } from './sign-up/sign-up.component';
import { SignUpApiService } from './common/api/sign-up/sign-up-service';
import { TicketsComponent } from './tickets/tickets.component';
import { TicketApiService } from './common/api/tickets/TicketService';
import { AddTicketComponent } from './add-ticket/add-ticket.component';
import { HeaderComponent } from './navigation/header/header.component';
import { LoginLayoutComponent } from './layouts/login-layout.component';
import { HomeLayoutComponent } from './layouts/home-layout.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SignUpComponent,
    TicketsComponent,
    AddTicketComponent,
    HeaderComponent,
    LoginLayoutComponent,
    HomeLayoutComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    NgbModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatIconModule,
    FormsModule,
    MatInputModule,
    MatButtonModule,
    MatToolbarModule,
    MatDividerModule,
    MatCardModule,
    MatProgressSpinnerModule,
    MatSelectModule,
    ReactiveFormsModule
  ],
  providers: [AuthApiService, WheatherForecastApiService, SignUpApiService, TicketApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
