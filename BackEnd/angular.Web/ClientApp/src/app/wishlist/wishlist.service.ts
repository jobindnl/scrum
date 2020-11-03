import { HttpClient, HttpParams } from '@angular/common/http';
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

  getWishlists(wishlistId: string): Observable<Iwishlist> {
    let params = new HttpParams().set('includeDetails', "true");
    return this.http.get<Iwishlist>(this.apiURL + '/' + wishlistId, {params: params});
  }

  getWishlistID(Id: string): Observable<Iwishlist> {
    return this.http.get<Iwishlist>(this.apiURL + '/' + Id);
  }

  createWishlist(wishlist: Iwishlist): Observable<Iwishlist> {
    return this.http.post<Iwishlist>(this.apiURL, wishlist);
  }

  deleteWishlist(Id: string): Observable<Iwishlist> {
    return this.http.delete<Iwishlist>(this.apiURL + '/' + Id);
  }

  updateWishlist(wishlist: Iwishlist): Observable<Iwishlist> {
    return this.http.put<Iwishlist>(this.apiURL + '/' + wishlist.id.toString(), wishlist);
  }

  getWishlistDetails(wishlist: Iwishlist): Observable<Iwishlist> {
    return this.http.get<Iwishlist>(this.apiURL + '/' + wishlist.wishlistDetails);
  }


}
