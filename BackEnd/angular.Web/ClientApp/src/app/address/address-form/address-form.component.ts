import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IAddress } from '../address';
import { AddressService } from '../address.service';

@Component({
  selector: 'app-credit-card-form',
  templateUrl: './address-form.component.html',
  styleUrls: ['./address-form.component.css']
})
export class AddressFormComponent implements OnInit {

  constructor(private fb: FormBuilder,
    private addressService: AddressService,
    private router: Router,
    private activatedRoute: ActivatedRoute) { }


  editMode: boolean = false;
  formGroup: FormGroup;
  addressId: number;


  ngOnInit() {
    this.formGroup = this.fb.group({
      id: 0,
      userId: 0,
      streetAddress: '',
      city: '',
      state: '',
      zipCode: '',
      country: ''
    });

    this.activatedRoute.params.subscribe(params => {
      if (params["id"] == undefined) {
        return;
      }
      this.editMode = true;

      this.addressId = params["id"];

      this.addressService.getAddress(this.addressId.toString())
        .subscribe(creditCard => this.loadForm(creditCard),
          error => console.error(error));
    });
  }

  loadForm(address: IAddress) {
    this.formGroup.patchValue({
      id: address.id,
      userId: address.userId,
      streetAddress: address.streetAddress,
      city: address.city,
      state: address.state,
      zipCode: address.zipCode,
      country: address.country
    })
  }

  save() {
    let address: IAddress = Object.assign({}, this.formGroup.value);
    console.table(address);

    if (this.editMode) {
      //edit credit card

      var x: number = +this.addressId;
      address.id = x;
      this.addressService.updateAddress(address)
        .subscribe(creditCard => this.onSaveSuccess(),
          error => console.error(error));
    } else {
      //add credit card

      this.addressService.createAddress(address)
        .subscribe(creditCard => this.onSaveSuccess(),
          error => console.error(error));
    }
  }

  onSaveSuccess() {
    this.router.navigate(["/address"]);
  }


}
