import { Component } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {

  constructor(
    public authService: AuthService,
    private router: Router) { }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    this.authService.logout();
  }

  login() {
    this.router.navigate(['/user/login']);
  }

  getUserName() {
    return sessionStorage.getItem('username');
  }
}
