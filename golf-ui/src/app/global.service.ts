import { Injectable } from '@angular/core';
import { LoginResponse } from './apiModels/login-response';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GlobalService {

  userData = new Subject();

  set loginData(value: string)
  {
    localStorage.setItem('user', value);
    this.userData.next(value);
  }

  get loginData()
  {
    return localStorage.getItem('user');

  }
  constructor()
  {
    this.loginData = localStorage.getItem('user');
  }
}
