import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUserInfo } from './UserInfo';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  // pass: Aa123456!
  private apiURL = this.baseUrl + "api/account";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  create(userInfo: IUserInfo): Observable<any> {
    return this.http.post<any>(this.apiURL + "/Create", userInfo);
  }

  login(userInfo: IUserInfo): Observable<any> {
    return this.http.post<any>(this.apiURL + "/Login", userInfo);
  }

  getToken(): string {
    return localStorage.getItem("token");
  }

  getExpirationToken(): string {
    return localStorage.getItem("tokenExpiration");
  }

  logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("tokenExpiration");
  }

  loggedIn(): boolean {

    var exp = this.getExpirationToken();

    if (!exp) {
      // token doesn't exist
      return false;
    }

    var now = new Date().getTime();
    var dateExp = new Date(exp);

    if (now >= dateExp.getTime()) {
      // token expired
      localStorage.removeItem('token');
      localStorage.removeItem('tokenExpiration');
      return false;
    } else {
      return true;
    }

  }

}
