import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  private headers: HttpHeaders;
  private accessPointUrl: string = 'https://localhost:44312/api/Author';

  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({ 'Content-Type': 'application/jason; charset=utf-8' });
  }

  public getAll() {
    return this.http.get(this.accessPointUrl + '/all', { headers: this.headers })
  }

  public searchBooks(payload) {
    return this.http.get("https://localhost:44312/api/Book/Author" + '/' + payload, { headers: this.headers })

  }
}
