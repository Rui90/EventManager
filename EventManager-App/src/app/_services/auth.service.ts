import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';
import { User } from '../_models/User';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = 'http://localhost:5000/api/user/';
  jwtHelper = new JwtHelperService();
  decodeToken: any;

  constructor(
    private http: HttpClient,
    private router: Router,
    private toastr: ToastrService) { }

  login(model: any) {
    return this.http.post(`${this.baseUrl}login`, model).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
          this.decodeToken = this.jwtHelper.decodeToken(user.token);
          sessionStorage.setItem('username', this.decodeToken.unique_name);
          console.log(this.decodeToken);
        }
      })
    );
  }

  register(model: User) {
    return this.http.post(`${this.baseUrl}register`, model);
  }

  loggedIn() {
    return !this.jwtHelper.isTokenExpired(localStorage.getItem('token'));
  }

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/user/login']);
    this.toastr.show('User logged out.');
  }

}
