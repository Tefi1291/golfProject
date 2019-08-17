import { CanActivate } from '@angular/router/src/utils/preactivation';
import { ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import {LoginResponse} from '../apiModels/login-response';
import { GlobalService } from '../global.service';

@Injectable()
export class AuthGuard implements CanActivate
{
    path: ActivatedRouteSnapshot[];
    route: ActivatedRouteSnapshot;
    
    constructor(
        private router: Router,
        private globalService : GlobalService
    ){}

    canActivate() {
        return this.globalService.loginData && this.globalService.loginData !== "" ;
    }
}