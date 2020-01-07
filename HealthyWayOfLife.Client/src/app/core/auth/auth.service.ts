import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '../../../environments/environment';
import { IUser } from './auth.model';

const tokenName = 'token';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private isLogged$ = new BehaviorSubject(false);
  private lastPath: string;
  private url = `${environment.apiBaseUrl}/api/authenticate`;
  private user: IUser;
  constructor(private http: HttpClient) {
  }

  public get isLoggedIn(): boolean {
    return this.isLogged$.value;
  }

  public login(user: IUser): Observable<any> {
    return this.http.post(`${this.url}/login`, user)
      .pipe(
        map((result: IUser) => {
          this.user = res.user;
          sessionStorage.setItem(tokenName, res.token);
          sessionStorage.setItem('username', res.user.username);
          sessionStorage.setItem('email', res.user.email);
          return res;
        }));
  }

  public logout() {
    return this.http.get(`${this.url}/logout`)
      .pipe(map((data) => {
        sessionStorage.clear();
        this.user = null;
        this.isLogged$.next(false);
        return of(false);
      }));
  }

  public signup(data) {
    return this.http.post(`${this.url}/signup`, data)
      .pipe(
        map((res: { user: any, token: string }) => {
          this.user = res.user;
          sessionStorage.setItem(tokenName, res.token);
          sessionStorage.setItem('username', res.user.username);
          sessionStorage.setItem('email', res.user.email);
          this.isLogged$.next(true);
          return this.user;
        }));
  }

  public get authToken(): string {
    return sessionStorage.getItem(tokenName);
  }

  public get userData(): Observable<any> {
    // send current user or load data from backend using token
    return this.loadUser();
  }

  private loadUser(): Observable<any> {
    // use request to load user data with token
    // it's fake and useing only for example
    if (sessionStorage.getItem('username') && sessionStorage.getItem('email')) {
      this.user = {
        username: sessionStorage.getItem('username'),
        email: sessionStorage.getItem('email'),
      };
    }
    return of(this.user);
  }
}
