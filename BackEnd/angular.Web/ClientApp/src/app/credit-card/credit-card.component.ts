import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account/account.service';
import { ICreditCard } from './credit-card'
import { CreditCardService } from './credit-card.service';

@Component({
  selector: 'app-credit-card',
  templateUrl: './credit-card.component.html',
  styleUrls: ['./credit-card.component.css']
})
export class CreditCardComponent implements OnInit {

  creditCards: ICreditCard[];

  constructor(private accountService: AccountService, private creditCardService: CreditCardService) { }

  ngOnInit() {
    this.loadData();
  }

  delete(creditCard: ICreditCard) {
    this.creditCardService.deleteCreditCard(creditCard.id.toString())
      .subscribe(creditCard => this.loadData(),
        error => this.handleError(error));
  }

  loadData() {
    this.creditCardService.getCreditCards()
      .subscribe(creditCardsfromapi => this.creditCards = creditCardsfromapi,
        error => this.handleError(error));
  }

  loggedIn() {
    return this.accountService.loggedIn();
  }

  handleError(error) {
    if (error && error.error) {
      alert(error.error["errors"]);
    }
  }
}
