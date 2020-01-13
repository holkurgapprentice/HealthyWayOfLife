import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '@core/';
import { BlankLayoutCardComponent } from '@shared/*';
import { IUser } from 'app/core/auth/auth.model';

const fg = dataItem =>
  new FormGroup({
    password: new FormControl('', [Validators.required]),
    email: new FormControl('', [
      Validators.required,
      Validators.pattern(
        '^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$',
      ),
      Validators.maxLength(20),
    ]),
  });

@Component({
  selector: 'app-login',
  styleUrls: ['../../../shared/blank-layout-card/blank-layout-card.component.scss'],
  templateUrl: './login.component.html',
})
export class LoginComponent extends BlankLayoutCardComponent implements OnInit {
  public loginForm: FormGroup;
  public email;
  public password;
  public emailPattern = '^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$';

  constructor(
    private authService: AuthService,
    private router: Router,
  ) {
    super();

    this.loginForm = fg(null);
    this.email = this.loginForm.get('email');
    this.password = this.loginForm.get('password');
  }

  public ngOnInit() {
    this.authService.logout();
  }

  public login() {
    if (this.loginForm.valid) {
      this.authService
        .login({
          email: this.loginForm.get('email').value,
          password: this.loginForm.get('password').value,
        } as IUser)
        .subscribe(
          () => {
            if (this.authService.lastPath) {
              this.router.navigate([this.authService.lastPath]);
            } else {
              this.router.navigate(['/app/dashboard']);
            }
          },
          (error: any) => {
            console.error(error);
          },
        );
    }
  }

  public onInputChange(event) {
    event.target.required = true;
  }
}
