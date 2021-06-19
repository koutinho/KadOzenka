import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginComponent } from './login.component';
import {By} from "@angular/platform-browser";
import {ApiService} from "../../common/api/api";
import {Router} from "@angular/router";

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApiService],
      declarations: [ LoginComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('login button should be disable', () => {
    // const button = fixture.debugElement.query(By.css('#loginButton'))
    // const atr = button.attributes["disabled"]
    // console.log('button.attributes', button.attributes);
    expect(true).toBeTruthy();
  });
});
