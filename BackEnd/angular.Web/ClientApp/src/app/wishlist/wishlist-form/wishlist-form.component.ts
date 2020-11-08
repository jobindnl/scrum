import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { IWishlist } from '../wishlist';
import { WishlistService } from '../wishlist.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-wishlist-form',
  templateUrl: './wishlist-form.component.html',
  styleUrls: ['./wishlist-form.component.css']
})
export class WishlistFormComponent implements OnInit {

  constructor(private fb: FormBuilder,
    private wishlistService: WishlistService,
    private router: Router,
    private activatedRoute: ActivatedRoute) { }

  editMode: boolean = false;
  formGroup: FormGroup;
  wishlistId: number;

  ngOnInit() {
    this.formGroup = this.fb.group({
      Id: 0,
      Name: '',
      UserId: 0
    });

    this.activatedRoute.params.subscribe(params => {
      if (params["id"] == undefined) {
        return;
      }
      this.editMode = true;
      this.wishlistId = params["id"];
      this.wishlistService.getWishlists(this.wishlistId.toString(), false)
        .subscribe(wishlist => this.loadForm(wishlist),
          error => this.handleError(error));
    })

  }

  loadForm(wishlist: IWishlist) {
    this.formGroup.patchValue({
      Id: wishlist.id,
      Name: wishlist.name,
      UserId: wishlist.userId
    })
  }

  save() {
    let wishlist: IWishlist = Object.assign({}, this.formGroup.value);
    console.table(wishlist);

    if (this.editMode) {
      wishlist.id = this.wishlistId;
      this.wishlistService.updateWishlist(wishlist)
        .subscribe(wishlist => this.onSaveSuccess(),
          error => this.handleError(error));
    } else {

      this.wishlistService.createWishlist(wishlist)
        .subscribe(wishlist => this.onSaveSuccess(),
          error => this.handleError(error));
    }
  }

  onSaveSuccess() {
    this.router.navigate(["/wishlist"]);
  }

  handleError(error) {
    if (error && error.error) {
      alert(error.error["errors"]);
    }
  }


}
