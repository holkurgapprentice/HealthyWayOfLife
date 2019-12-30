import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { ILoginModel } from './login.model';

@Injectable()
export class LoginService {

  url: string;

  constructor(private http: HttpClient) {
    this.url = environment.apiBaseUrl;
  }

  public sendLoginReguest(loginParams: ILoginModel): Observable<any> {
    return this.http.post(this.url + 'Authenticate\\Login', loginParams);
  }
}
