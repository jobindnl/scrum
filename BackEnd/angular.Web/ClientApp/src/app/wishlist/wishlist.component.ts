import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account/account.service';
import { Iwishlist } from './wishlist';
import { WishlistService } from './wishlist.service';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.css']
})
export class WishlistComponent implements OnInit {

  wishlists: Iwishlist[];

  constructor(private wishlistService: WishlistService, private accountService: AccountService) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.wishlistService.getWishlist()
      .subscribe(wishlistsfromapi => this.wishlists = wishlistsfromapi,
      error => console.error(error));
  }

  loggedIn() {
    return this.accountService.loggedIn();
  }

}
