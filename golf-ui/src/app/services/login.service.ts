import { Injectable, ɵConsole } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Login {

  _httpClient: HttpClient;
  api_url = "http:///localhost:5000/api/authentication/?";
  constructor(httpClient: HttpClient) 
  {
    this._httpClient = httpClient;
  }

  login(username:string, password:string): Observable<object>
  {
    let endpoint = `${this.api_url}username=${username}&password=${password}`;
    console.log(`SENDING REQUEST API... ${endpoint}`);
    return this._httpClient.get(endpoint);
  }
}
