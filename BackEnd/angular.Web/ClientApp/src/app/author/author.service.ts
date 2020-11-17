import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  private headers: HttpHeaders;
  private accessPointUrl = this.baseUrl + 'api/Author'
  //private accessPointUrl: string = this.baseUrl +'https://localhost:44312/api/Author'; //(Original but must be dynamic)

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.headers = new HttpHeaders({ 'Content-Type': 'application/jason; charset=utf-8' });
  }

  public getAll() {
    return this.http.get(this.accessPointUrl + '/all', { headers: this.headers })
  }

  public searchBooks(payload) {
    return this.http.get(this.baseUrl + "api/Book/Author" + '/' + payload, { headers: this.headers })
    //return this.http.get("https://localhost:44312/api/Book/Author" + '/' + payload, { headers: this.headers }) //(Original but must be dynamic)
  }
}
