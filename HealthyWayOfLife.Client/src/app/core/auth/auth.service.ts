import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { IUser } from './auth.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private isLogged$ = new BehaviorSubject(false);
  private lastPath: string;
  private url = `${environment.apiBaseUrl}authenticate`;
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
          this.user = result;
          sessionStorage.setItem('username', result.name);
          sessionStorage.setItem('email', result.email);
          this.isLogged$.next(true);
          return result;
        }));
  }

  public logout() {
    return this.http.get(`${this.url}/logout`)
      .pipe(map((data) => {
        sessionStorage.removeItem('username');
        sessionStorage.removeItem('email');
        this.user = null;
        this.isLogged$.next(false);
        return data;
      }));
  }

  public signup(data) {
    return this.http.post(`${this.url}/signup`, data)
      .pipe(
        map((result: IUser) => {
          this.user = result;
          sessionStorage.setItem('username', result.name);
          sessionStorage.setItem('email', result.email);
          this.isLogged$.next(true);
          return result;
        }));
  }

  public get authToken(): string {
    return this.user.token;
  }

  public get userData(): Observable<any> {
    return of(this.user);
  }
}
