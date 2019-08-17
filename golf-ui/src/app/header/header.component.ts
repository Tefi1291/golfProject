import { Component, OnInit } from '@angular/core';
import { LoginResponse } from '../apiModels/login-response';
import { Router } from '@angular/router';
import { GlobalService } from '../global.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  title: string = "GOLF OMS";
  private _loginResponse: LoginResponse;
  private username: string = "";

  constructor(
    private _router: Router,
    private _globalService: GlobalService
  ) 
  {
    this.getUsername(this._globalService.loginData);
    this._globalService.userData.subscribe(this.getUsername);

  }

  ngOnInit() {

  }

  getUsername(next: string)
  {
    
      if(next.length == 0)
      {
        this.username = "";
      }

      else{
        let userDataResponse = JSON.parse(next) as LoginResponse;
        this.username = (userDataResponse) ?
        userDataResponse.username :
          "";
      }
  }

  logout(): void {

    this._globalService.loginData = "";
    this._router.navigate(['login']);
  }
}
