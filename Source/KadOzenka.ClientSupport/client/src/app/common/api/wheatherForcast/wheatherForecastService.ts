import { HttpClient, HttpHandler, HttpHeaders, HttpResponse } from "@angular/common/http"
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { Http } from "../httpMethods/methods";
import { WeatherForecast } from "./WeatherForecast";

@Injectable()
export class WheatherForecastApiService {
    private http: Http;
    constructor(httpClient: HttpClient){
        this.http = new Http(httpClient);
    }

    getForecasts(): Observable<WeatherForecast[]> {
        return this.http.get<WeatherForecast[]>("WeatherForecast");
    }
}