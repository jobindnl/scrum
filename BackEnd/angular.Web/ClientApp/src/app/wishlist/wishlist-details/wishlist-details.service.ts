import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IWishlistDetail } from './wishlist-details';



@Injectable({
  providedIn: 'root'
})
export class WishlistDetailsService {

  private apiURL = this.baseUrl + "api/wishlistdetail";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getWishlistDetails(wishlistId: string): Observable<IWishlistDetail[]> {
    let params = new HttpParams().set('WishListId', wishlistId);
    return this.http.get<IWishlistDetail[]>(this.apiURL, { params: params });
  }

  getWishlistDetail(wishlistdetailId: string): Observable<IWishlistDetail> {
    let params = new HttpParams().set('includeDetails', "true");
    return this.http.get<IWishlistDetail>(this.apiURL + '/' + wishlistdetailId);
  }

  createWishlistDetail(wishlistdetail: IWishlistDetail): Observable<IWishlistDetail> {
    return this.http.post<IWishlistDetail>(this.apiURL + '/' + wishlistdetail.id, wishlistdetail);
  }

  deleteWishlistDetail(wishlistdetailId: string): Observable<IWishlistDetail> {
    return this.http.delete<IWishlistDetail>(this.apiURL + '/' + wishlistdetailId);
  }

  updateWishlistDetail(wishlistdetail: IWishlistDetail): Observable<IWishlistDetail> {
    return this.http.put<IWishlistDetail>(this.apiURL + '/' + wishlistdetail.id, wishlistdetail);
  }

}
