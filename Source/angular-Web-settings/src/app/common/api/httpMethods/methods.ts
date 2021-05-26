import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Settings } from "src/app/settings";

export class Http {

    constructor(private httpClient: HttpClient){}

    get<O>(endPoint: string): Observable<O>{
        return this.httpClient.get<O>(Settings.apiUrl + endPoint)
    }

}