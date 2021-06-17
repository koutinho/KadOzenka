import { HttpClient, HttpHandler, HttpHeaders, HttpResponse } from "@angular/common/http"
import { from, Observable } from "rxjs";
import { Http } from "./httpMethods/methods";
import { IsignIn } from "./Interface/auth.interface";


export class AuthApiService {
    private http: Http;
    constructor(httpClient: HttpClient){
        this.http = new Http(httpClient);
    }
    
    checkAuth(): Observable<boolean>{
        return this.http.get<boolean>("api/Auth/CheckAuthorize")
    }

    signIn(model: IsignIn): Observable<boolean>{
        return this.http.post<IsignIn, boolean>("api/Auth/login", model)
    }

    logOut(): Observable<boolean>{
        return this.http.get<boolean>("api/Auth/logout");
    }
}