import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account/account.service';
import { IAddress } from './address'
import { AddressService } from './address.service';

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.css']
})
export class AddressComponent implements OnInit {

  addresses: IAddress[];

  constructor(private accountService: AccountService, private addressService: AddressService) { }

  ngOnInit() {
    this.loadData();
  }

  delete(address: IAddress) {
    this.addressService.deleteAddress(address.id.toString())
      .subscribe(address => this.loadData(),
      error => console.error(error));
  }

  loadData() {
    this.addressService.getAddresses()
      .subscribe(addressesfromapi => this.addresses = addressesfromapi,
        error => console.error(error));
    //alert(this.addresses);
  }

  loggedIn() {
    return this.accountService.loggedIn();
  }

}
