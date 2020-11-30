import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IWishlistDetail } from '../wishlist/wishlist-details/wishlist-details';
import { IBook } from './book';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  private apiURL = this.baseUrl + "api/book";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getBooks(): Observable<IBook[]> {
    return this.http.get<IBook[]>(this.apiURL);
  }

  getBook(bookId: string): Observable<IBook> {
    return this.http.get<IBook>(this.apiURL + '/' + bookId);
  }

  createBook(book: IBook): Observable<IBook> {
    return this.http.post<IBook>(this.apiURL, book);
  }

  updateBook(book: IBook): Observable<IBook> {
    return this.http.put<IBook>(this.apiURL + '/' + book.id.toString(), book);
  }

  deleteBook(bookId: string): Observable<IBook> {
    return this.http.delete<IBook>(this.apiURL + '/' + bookId);
  }

}
