import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Iwishlist } from './wishlist';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {

  private apiURL = this.baseUrl + "api/wishlist";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getWishlist(): Observable<Iwishlist[]> {
    return this.http.get<Iwishlist[]>(this.apiURL);
  }

  getWishlistID(Id: string): Observable<Iwishlist> {
    return this.http.get<Iwishlist>(this.apiURL + '/' + Id);
  }

  createWishlist(wishlist: Iwishlist): Observable<Iwishlist> {
    return this.http.post<Iwishlist>(this.apiURL, wishlist);
  }

  deleteBook(Id: string): Observable<Iwishlist> {
    return this.http.delete<Iwishlist>(this.apiURL + '/' + Id);
  }

}
