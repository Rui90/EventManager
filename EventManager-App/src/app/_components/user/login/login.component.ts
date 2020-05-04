import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public title = 'Login';
  public model: any = {};

  constructor(
    public router: Router,
    private toastr: ToastrService,
    private authService: AuthService) { }

  ngOnInit() {
    if (this.authService.loggedIn()) {
      this.router.navigate(['/dashboard']);
    }
  }

  login(): void {
    this.authService.login(this.model).subscribe(
      () => {
        this.toastr.success('Logged in with success');
        this.router.navigate(['/dashboard']);
      },
      (error) => {
        this.toastr.error('Login failed');
      }
    );
  }
}
