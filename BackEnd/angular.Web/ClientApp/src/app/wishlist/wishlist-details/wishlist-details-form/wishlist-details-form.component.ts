import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { WishlistService } from '../../wishlist.service';
import { IWishlistDetail } from '../wishlist-details';
import { WishlistDetailsService } from '../wishlist-details.service';

@Component({
  selector: 'app-wishlist-details-form',
  templateUrl: './wishlist-details-form.component.html',
  styleUrls: ['./wishlist-details-form.component.css']
})
export class WishlistDetailsFormComponent implements OnInit {

  constructor(private fb: FormBuilder,
    private wishlistDetailsService: WishlistDetailsService,
    private wishlistService: WishlistService,
    private router: Router,
    private activatedRoute: ActivatedRoute) { }

  editMode: boolean = false;
  formGroup: FormGroup;
  wishListId: number;
  wishlist: IWishlistDetail;

  ngOnInit() {
    this.formGroup = this.fb.group({
      wishlistId: 0,
    });

    this.activatedRoute.params.subscribe(params => {
      if (params["id"] == undefined) {
        return;
      }

      this.editMode = true;
      this.wishListId = params["id"];
      this.wishlistDetailsService.getWishlistDetail(this.wishListId.toString())
        .subscribe(wishlist => this.loadForm(wishlist),
          error => this.handleError(error));
    })

  }

  loadForm(wishlist: IWishlistDetail) {
    this.formGroup.patchValue({
      wishlistId: wishlist.wishlistid
    })
  }

  save() {
    let wishlist: IWishlistDetail = Object.assign({}, this.formGroup.value);
    console.table(wishlist.wishlistid);

    if (this.editMode) {
      var x: number = +this.wishListId;
      wishlist.wishlistid = x;
      wishlist.wishlistid = this.wishListId;
      this.wishlistDetailsService.updateWishlistDetail(wishlist)
        .subscribe(wishlist => this.onSaveSuccess(),
          error => this.handleError(error));
    }
  }

  onSaveSuccess() {
    this.router.navigate(["/wishlist-details"]);
  }

   handleError(error) {
    if (error && error.error) {
      alert(error.error["errors"]);
    }
  }
}
