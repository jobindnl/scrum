import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ICreditCard } from '../credit-card';
import { CreditCardService } from '../credit-card.service';
import { CreditCardValidators } from './creditcard.validator';
  
@Component({
  selector: 'app-credit-card-form',
  templateUrl: './credit-card-form.component.html',
  styleUrls: ['./credit-card-form.component.css']
})
export class CreditCardFormComponent implements OnInit {

  constructor(private fb: FormBuilder,
    private creditCardService: CreditCardService,
    private router: Router,
    private activatedRoute: ActivatedRoute ) { }


  editMode: boolean = false;
  formGroup: FormGroup;
  creditCardId: number;


  ngOnInit() {

    this.formGroup = this.fb.group({
      name: ['', Validators.required],
      number: [0, Validators.required, CreditCardValidators.lenghtnot16],
      expMonth: [0, Validators.required, CreditCardValidators.monthnotbetween1_12],
      expYear: [0, Validators.required, CreditCardValidators.nonexpiredcard],
      cvv: [0, Validators.required, CreditCardValidators.lenghtnot3],
    });

    this.activatedRoute.params.subscribe(params => {
      if (params["id"] == undefined) {
        return;
      }
      this.editMode = true;

      this.creditCardId = params["id"];

      this.creditCardService.getCreditCard(this.creditCardId.toString())
        .subscribe(creditCard => this.loadForm(creditCard),
          error => console.error(error));
    });
  }

  loadForm(creditCard: ICreditCard) {
    this.formGroup.patchValue({
      name: creditCard.name,
      number: creditCard.number,
      expMonth: creditCard.expMonth,
      expYear: creditCard.expYear,
      cvv: creditCard.cvv
    })
  }

  save() {
    let creditCard: ICreditCard = Object.assign({}, this.formGroup.value);
    console.table(creditCard);

    if (this.editMode) {
      //edit credit card
      
      var x: number = +this.creditCardId;
      creditCard.id = x;
      this.creditCardService.updateCreditCard(creditCard)
        .subscribe(creditCard => this.onSaveSuccess(),
          error => console.error(error));
    } else {
      //add credit card

      this.creditCardService.createCreditCard(creditCard)
        .subscribe(creditCard => this.onSaveSuccess(),
          error => console.error(error));
    }
  }

  onSaveSuccess() {
    this.router.navigate(["/credit-card"]);
  }

  findInvalidControls() {
    const invalid = [];
    const controls = this.formGroup.controls;
    for (const name in controls) {
      if (controls[name].invalid) {
        invalid.push(name);
      }
    }
    return invalid;
  }

}
