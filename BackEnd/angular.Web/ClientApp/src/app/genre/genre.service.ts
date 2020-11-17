import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'; 


@Injectable({
  providedIn: 'root'
})
export class GenreService {
  private headers: HttpHeaders;
  private accessPointUrl = this.baseUrl + 'api/Genre'
  //private accessPointUrl: string = 'https://localhost:44312/api/Genre'; 

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.headers = new HttpHeaders({ 'Content-Type': 'application/jason; charset=utf-8' });
  }

  public getAll() {
    return this.http.get(this.accessPointUrl + '/all', { headers: this.headers })
  }

  public searchBooks(payload) {
    return this.http.get(this.baseUrl +"api/Book/Genre" + '/' + payload, { headers: this.headers })
    //return this.http.get("https://localhost:44312/api/Book/Genre" + '/' + payload, { headers: this.headers })

  }

}


