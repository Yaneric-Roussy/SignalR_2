import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css']
})
export class AuthenticationComponent implements OnInit {

  email="test1@test.com";

  constructor(public authentication:AuthenticationService) { }

  ngOnInit() {
  }

  async registerAndLogin(){
    let registerData = {
      email : this.email,
      password : "Passw0rd!",
      passwordConfirm : "Passw0rd!",
    }
    await this.authentication.registerAndLogin(registerData);
  }

  async login(){
    let loginData = {
      email : this.email,
      password : "Passw0rd!"
    }
    await this.authentication.login(loginData);
    console.log(this.authentication);
  }

  async logout(){
    await this.authentication.logout();
  }

  async testAuthorize() {
    await this.authentication.testAuthorize();
  }

}
