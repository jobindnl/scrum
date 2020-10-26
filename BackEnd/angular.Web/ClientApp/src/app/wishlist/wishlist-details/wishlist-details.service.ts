import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IBook } from '../../book/book';
import { IwishlistDetails } from './wishlist-details';


@Injectable({
  providedIn: 'root'
})
export class WishlistDetailsService {

  private apiURL = this.baseUrl + "api/wishlist-details";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getWishlistItems(): Observable<IwishlistDetails[]> {

    //TODO: map obtained result. (pipe and map functions)
    return this.http.get<IwishlistDetails[]>(this.apiURL);
  }

  addToWishList(book: IBook): Observable<any> {
    return this.http.post(this.apiURL, {book} );
  }

  removeFromWishlist(BookId: number): Observable<IwishlistDetails> {
    return this.http.delete<IwishlistDetails>(this.apiURL + '/' + BookId);
  }
}
