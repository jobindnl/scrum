import { Component, OnInit } from '@angular/core';
import { WishlistDetailsService } from './wishlist-details.service';
import { IWishlist } from '../wishlist'
import { WishlistService } from '../wishlist.service';
import { ActivatedRoute } from '@angular/router';
import { IWishlistDetail } from './wishlist-details';
import { AccountService } from '../../account/account.service';

@Component({
  selector: 'app-wishlist-details',
  templateUrl: './wishlist-details.component.html',
  styleUrls: ['./wishlist-details.component.css']
})
export class WishlistDetailsComponent implements OnInit {

  wishlistDetails: IWishlistDetail[];
  wishlistId: number;

  constructor(private wishlistDetailsService: WishlistDetailsService, private accountService: AccountService, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      if (params["id"] == undefined) {
        return;
      }

      this.wishlistId = params["id"];

      this.wishlistDetailsService.getWishlistDetails(this.wishlistId.toString())
        .subscribe(wishlist => this.loadData(),
          error => console.error(error));

    })
  }

  loadData() {
    this.wishlistDetailsService.getWishlistDetails(this.wishlistId.toString())
      .subscribe(wishlistDetailsfromapi => this.wishlistDetails = wishlistDetailsfromapi,
        error => console.error(error));
  }

  loggedIn() {
    return this.accountService.loggedIn();
  }

  delete(wishlistDetail: IWishlistDetail) {
    this.wishlistDetailsService.deleteWishlistDetail(wishlistDetail.id.toString())
      .subscribe(wishlist => this.loadData(),
        error => console.error(error));
  }

}
