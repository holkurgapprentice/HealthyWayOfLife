import { Injectable } from '@angular/core';
import { AuthService } from '@core/*';
import { IUser } from 'app/core/auth/auth.model';
import { Observable } from 'rxjs';

@Injectable()
export class LoginService {

  constructor(private authService: AuthService) {
  }

  public sendLoginReguest(loginParams: IUser): Observable<any> {
    return this.authService.login(loginParams);
  }
}
