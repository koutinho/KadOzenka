import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { JwtHelperService } from "@auth0/angular-jwt";
import { Observable, of } from "rxjs";
import { catchError, map } from "rxjs/operators";

@Injectable()
export class AuthGuard implements CanActivate{

    constructor(private jwtHelper: JwtHelperService, private router: Router){}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> {
        const token = localStorage.getItem("jwt")

        if (token && !this.jwtHelper.isTokenExpired(token)) {
            return of(true);
        }

        return this.router.navigate(['login']);
    }
}