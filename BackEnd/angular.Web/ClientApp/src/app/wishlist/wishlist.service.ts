import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IWishlist } from './wishlist';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {

  private apiURL = this.baseUrl + "api/wishlist";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getWishlist(): Observable<IWishlist[]> {
    return this.http.get<IWishlist[]>(this.apiURL);
  }

  getWishlists(wishlistId: string, includeDetails: boolean): Observable<IWishlist> {
    let params = new HttpParams();
    if (includeDetails)
      params.set('includeDetails', "true")
    return this.http.get<IWishlist>(this.apiURL + '/' + wishlistId, {params: params});
  }

  getWishlistID(Id: string): Observable<IWishlist> {
    return this.http.get<IWishlist>(this.apiURL + '/' + Id);
  }

  createWishlist(wishlist: IWishlist): Observable<IWishlist> {
    return this.http.post<IWishlist>(this.apiURL, wishlist);
  }

  deleteWishlist(Id: string): Observable<IWishlist> {
    return this.http.delete<IWishlist>(this.apiURL + '/' + Id);
  }

  updateWishlist(wishlist: IWishlist): Observable<IWishlist> {
    return this.http.put<IWishlist>(this.apiURL + '/' + wishlist.id.toString(), wishlist);
  }

  getWishlistDetails(wishlist: IWishlist): Observable<IWishlist> {
    return this.http.get<IWishlist>(this.apiURL + '/' + wishlist.wishlistDetails);
  }


}
