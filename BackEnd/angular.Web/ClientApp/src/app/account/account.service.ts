import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUserInfo } from './UserInfo';
import jwt_decode from 'jwt-decode';
import { ITokenInfo } from './tokeninfo';
import { IForgotPassword } from './forgot-password/forgot-password';
import { IVerifyTokenResetPassword } from './verify-token-reset-password/verify-token-reset-password';
import { IPwdChange } from './pwd-change/pwdchange';

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

  changePassword(pwdchange: IPwdChange): Observable<any> {
    return this.http.post<any>(this.apiURL + "/ChangePassword", pwdchange);
  }

  forgotPassword(forgotpassword: IForgotPassword): Observable<any> {
    return this.http.post<any>(this.apiURL + "/ForgotPassword", forgotpassword);
  }

  verifyTokenResetPassword(verifytokenresetpassword: IVerifyTokenResetPassword): Observable<any> {
    return this.http.post<any>(this.apiURL + "/verify-token-reset-password", verifytokenresetpassword);
  }

  login(userInfo: IUserInfo): Observable<any> {
    return this.http.post<any>(this.apiURL + "/Login", userInfo);
  }

  getToken(): string {
    return localStorage.getItem("token");
  }

  getDecodedAccessToken(): ITokenInfo {
    try {
      var token = jwt_decode(this.getToken());
      return <ITokenInfo>token
    }
    catch (Error) {
      return null;
    }
  }

  isAdmin(): boolean {
    let token = this.getDecodedAccessToken()
    if (token == null) {
      return false;
    }
    if (token.Roles == null) {
      return false;
    }
    let roles = <string[]>JSON.parse(token.Roles);
    return roles.find(x => x == "Admin") != null;
  }

  refreshToken(): string {
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
