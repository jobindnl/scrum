import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Iwishlist } from '../wishlist';
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
    private router: Router) { }

  formGroup: FormGroup;

  ngOnInit() {
    this.formGroup = this.fb.group({
      Id: 0,
      Name: '',
      UserId: 0
    });
  }

  save() {
    let wishlist: Iwishlist = Object.assign({}, this.formGroup.value);
    console.table(wishlist);

    this.wishlistService.createWishlist(wishlist)
      .subscribe(wishlist => this.onSaveSuccess(),
        error => console.error(error));
  }

  onSaveSuccess() {
    this.router.navigate(["/wishlist"]);
  }

}
