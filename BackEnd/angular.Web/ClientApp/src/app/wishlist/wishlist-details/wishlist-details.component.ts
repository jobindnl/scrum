import { Component, OnInit } from '@angular/core';
import { WishlistDetailsService } from './wishlist-details.service';
import { IwishlistDetails } from './wishlist-details';
import { Iwishlist } from '../wishlist'
import { WishlistService } from '../wishlist.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-wishlist-details',
  templateUrl: './wishlist-details.component.html',
  styleUrls: ['./wishlist-details.component.css']
})
export class WishlistDetailsComponent implements OnInit {

  wishlistDetails: IwishlistDetails[];
  wishlistId: number;

  constructor(private wishlistDetails: WishlistDetailsService, private wishlistService: WishlistService, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      if (params["id"] == undefined) {
        return;
      }

      this.wishlistId = params["id"];

      this.wishlistService.getWishlists(this.wishlistId.toString())
        .subscribe(wishlist => this.loadData(wishlist),
          error => console.error(error));

    })
  }

  loadData(wishlist: Iwishlist) {
    this.wishlistService.getWishlist()
      .subscribe(wishlistsfromapi => this.wishlists = wishlistsfromapi,
        error => console.error(error));
  }

}
