import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUserProfile } from './user-profile';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService {

  private apiURL = this.baseUrl + "api/UserProfile";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getUserProfile(userProfileId: string): Observable<IUserProfile> {
    return this.http.get<IUserProfile>(this.apiURL + '/' + 2);
  }

  updateUserProfile(userProfile: IUserProfile): Observable<IUserProfile> {
    return this.http.put<IUserProfile>(this.apiURL + '/' + 2, userProfile);
  }

}
