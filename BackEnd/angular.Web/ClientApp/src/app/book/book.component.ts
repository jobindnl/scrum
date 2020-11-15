import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account/account.service';
import { ITokenInfo } from '../account/tokeninfo';
import { IBook } from './book'
import { BookService } from './book.service';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {

  books: IBook[];

  constructor(private accountService: AccountService, private bookService: BookService) { }

  ngOnInit() {
    this.loadData();
  }

  delete(book: IBook) {
    this.bookService.deleteBook(book.id.toString())
      .subscribe(book => this.loadData(),
        error => console.error(error));
  }

  loadData() {
    this.bookService.getBooks()
      .subscribe(booksfromapi => this.books = booksfromapi,
        error => console.error(error));
  }

  loggedIn() {
    return this.accountService.loggedIn();
  }

  isAdmin() {
    return this.accountService.isAdmin();
  }

}
