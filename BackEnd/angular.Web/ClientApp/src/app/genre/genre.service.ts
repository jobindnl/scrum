import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'; 


@Injectable({
  providedIn: 'root'
})
export class GenreService {
  private headers: HttpHeaders;
  private accessPointUrl: string = 'https://localhost:44312/api/Genre'; 

  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({ 'Content-Type': 'application/jason; charset=utf-8' });
  }

  public getAll() {
    return this.http.get(this.accessPointUrl + '/all', { headers: this.headers })
  }

  public searchBooks(payload) {
    return this.http.get("https://localhost:44312/api/Book/Genre" + '/' + payload, { headers: this.headers })

  }

}


