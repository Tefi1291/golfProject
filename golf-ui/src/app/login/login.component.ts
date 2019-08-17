import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Login } from '../services/login.service';
import { LoginResponse } from '../apiModels/login-response';
import { isNullOrUndefined } from 'util';
import { Router } from '@angular/router';
import { GlobalService } from '../global.service';
import { ErrorApi } from '../apiModels/error';
//import { isNullOrUndefined } from 'util';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login_service: Login;
  LoginForm = new FormGroup({
    'username' : new FormControl(),
    'password' : new FormControl()
  });

  loginErrorMsg: string = "";

  constructor(
    private router: Router,
    loginService: Login,
    private globalService : GlobalService
    ) 
  {
    this.login_service = loginService;

  }

  ngOnInit() {
  }

  submitLogin():void
  {
    console.log(this.LoginForm);
    if(this.LoginForm.valid)
    {
      let username = this.LoginForm.get('username').value;
      let pass = this.LoginForm.get('password').value;
      console.log("SUBMIT FORM...");
      this.login_service.login(username, pass).subscribe(
        (data:any) =>
        {
          var loginResponse = data as LoginResponse;
          console.log(data);

          if(!isNullOrUndefined(loginResponse.username))
          {
            
            this.globalService.loginData = JSON.stringify(loginResponse); 
            this.router.navigate(['orders']);
          }
          else
          {
            //login error
            let errorInfo = data as ErrorApi;
            if(errorInfo)
            {
              this.loginErrorMsg = `Login error: ${errorInfo.description}`;
            }
          }

        },
        (error)=>
        {
          console.log(error);

        }
      );
      
    }
  }

}
