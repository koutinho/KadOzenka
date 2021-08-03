import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { Settings } from "src/settings";


const options = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json',
         'Accept': 'application/json'
    }), withCredentials: true
}

export class Http {

    constructor(private httpClient: HttpClient){}

    get<O>(endPoint: string): Observable<O>{
        return this.httpClient.get<O>(Settings.apiUrl + endPoint, {withCredentials: true})
    }

    post<I, O>(endPoint: string, data: I): Observable<O>{
        return this.httpClient.post<O>(Settings.apiUrl + endPoint, data, options)
    }
}