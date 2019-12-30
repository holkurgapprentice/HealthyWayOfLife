import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '@core/';
import { BlankLayoutCardComponent } from '@shared/*';
import { ILoginModel } from './login.model';
import { LoginService } from './login.service';

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
  providers: [LoginService],
})
export class LoginComponent extends BlankLayoutCardComponent implements OnInit {
  public loginForm: FormGroup;
  public email;
  public password;
  public emailPattern = '^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$';
  public error: string;

  constructor(
    private authService: AuthService,
    private loginService: LoginService,
    private router: Router,
  ) {
    super();

    this.loginForm = fg(null);
    this.email = this.loginForm.get('email');
    this.password = this.loginForm.get('password');
  }

  public ngOnInit() {
    this.authService.logout();
    this.loginForm.valueChanges.subscribe(() => {
      this.error = null;
    });
  }

  public login() {
    this.error = null;
    if (this.loginForm.valid) {
      this.loginService
        .sendLoginReguest({
          email: this.loginForm.get('email').value,
          password: this.loginForm.get('password').value,
          languageType: 1
        } as ILoginModel)
        .subscribe(x =>
          console.log(x));
      console.log(this.loginForm.getRawValue());
      // this.authService
      //   .login(this.loginForm.getRawValue())
      //   .subscribe(
      //     res => this.router.navigate(['/app/dashboard']),
      //     error => (this.error = error.message),
      //   );
    }
  }

  public onInputChange(event) {
    event.target.required = true;
  }
}
