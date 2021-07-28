import { Component, OnInit } from '@angular/core';
import { WheatherForecastApiService } from '../common/guards/api/wheatherForcast/wheatherForecastService';
import { WeatherForecast } from '../common/guards/api/wheatherForcast/WeatherForecast';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public forecasts: WeatherForecast[] = [];
  
  constructor(private wheatherForecastApi: WheatherForecastApiService) {
    wheatherForecastApi.getForecasts().subscribe(
      (res) => this.forecasts = res);
  }

  ngOnInit(): void {
  }
}
