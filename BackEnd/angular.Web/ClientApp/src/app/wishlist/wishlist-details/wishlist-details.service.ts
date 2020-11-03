import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IBook } from '../../book/book';
import { Iwishlist } from '../wishlist';
import { IwishlistDetails } from './wishlist-details';



@Injectable({
  providedIn: 'root'
})
export class WishlistDetailsService {

  private apiURL = this.baseUrl + "api/wishlist-details";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  addToWishList(book: IBook): Observable<IwishlistDetails> {
    return this.http.post<IwishlistDetails>(this.apiURL + '/' + '43', { book }); // 43 is a wishlist Id I created.
  }

  removeFromWishlist(BookId: number): Observable<IwishlistDetails> {
    return this.http.delete<IwishlistDetails>(this.apiURL + '/' + BookId);
  }

}
