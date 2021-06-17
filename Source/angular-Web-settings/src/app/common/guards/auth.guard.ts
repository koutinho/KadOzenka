import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError, map } from "rxjs/operators";
import { ApiService } from "../api/api";

@Injectable()
export class AuthGuard implements CanActivate{

    constructor(private api: ApiService, private router: Router){}


    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> {
        return this.api.authApi.checkAuth().pipe(map((result: boolean) => {
            if(result) {
                return true;
            }
            return false;
        }), catchError(error => {
            console.log('server error', error);
            return this.router.navigate(['signIn']);
        }));
    }

}