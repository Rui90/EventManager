import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/User';
import { AuthService } from 'src/app/_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registerForm: FormGroup;
  user: User = new User();

  constructor(
    public fb: FormBuilder,
    private toastr: ToastrService,
    private authService: AuthService,
    private router: Router) { }

  ngOnInit() {
    this.validation();
    console.log(this.registerForm);
  }

  validation() {
    this.registerForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      userName: ['', Validators.required],
      passwords: this.fb.group({
        password: ['', [Validators.required, Validators.minLength(4)]],
        confirmPassword: ['', Validators.required]
      })
    }, { validator: this.matchPassword });
  }

  signUp() {
    if (this.registerForm.valid) {
      this.user = Object.assign({password: this.registerForm.get('passwords.password').value}, this.registerForm.value);
      this.authService.register(this.user).subscribe(
        () => {
          this.router.navigate(['/user/login']);
          this.toastr.success('Registration succeeded');
        },
        (errors) => {
          errors.array.forEach(element => {
            switch (element.code) {
              case 'DuplicatedUserName':
                this.toastr.error('User already exists.');
                break;
              default:
                this.toastr.error('An error occurred');
                break;
            }
          });
        }
      );
    }
  }

  matchPassword(fb: FormGroup) {
    const confirmPassword = fb.get('confirmPassword');
    if (confirmPassword && (confirmPassword.errors === null || 'mismatch' in confirmPassword.errors)) {
      if (fb.get('confirmPassword').value !== confirmPassword.value) {
        confirmPassword.setErrors({mismatch: true});
      } else {
        confirmPassword.setErrors(null);
      }
    }
  }

}
