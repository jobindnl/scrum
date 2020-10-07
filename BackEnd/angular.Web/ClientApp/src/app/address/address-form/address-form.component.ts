import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IAddress } from '../address';
import { AddressService } from '../address.service';
  
@Component({
  selector: 'app-address-form',
  templateUrl: './address-form.component.html',
  styleUrls: ['./address-form.component.css']
})
export class AddressFormComponent implements OnInit {

  constructor(private fb: FormBuilder,
    private addressService: AddressService,
    private router: Router,
    private activatedRoute: ActivatedRoute ) { }


  editMode: boolean = false;
  formGroup: FormGroup;
  addressId: number;


  ngOnInit() {
    this.formGroup = this.fb.group({
      streetAddress: '',
      city: 0,
      state: '',
      zipcode:'',
      country: 0,
    });

    this.activatedRoute.params.subscribe(params => {
      if (params["id"] == undefined) {
        return;
      }
      this.editMode = true;

      this.addressId = params["id"];

      this.addressService.getAddress(this.addressId.toString())
        .subscribe(address => this.loadForm(address),
          error => console.error(error));
    });
  }

  loadForm(address: IAddress) {
    this.formGroup.patchValue({
      streeetAddress: address.streetAddress,
      city: address.city,
      state: address.state,
      zipcode: address.zipCode,
      country: address.country
    })
  }

  save() {
    let address: IAddress = Object.assign({}, this.formGroup.value);
    console.table(address);

    if (this.editMode) {
      //edit address
      
      var x: number = +this.addressId;
      address.id = x;
      this.addressService.updateAddress(address)
        .subscribe(address => this.onSaveSuccess(),
          error => console.error(error));
    } else {
      //add address

      this.addressService.createAddress(address)
        .subscribe(address => this.onSaveSuccess(),
          error => console.error(error));
    }
  }

  onSaveSuccess() {
    this.router.navigate(["/address"]);
  }


}
