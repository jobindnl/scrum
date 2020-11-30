import { AbstractControl, ValidationErrors } from '@angular/forms';


export class CreditCardValidators {

  static lenghtnot16(control: AbstractControl): Promise<ValidationErrors | null> {
    return new Promise((resolve, reject) => {
      let exp = new RegExp("^[0-9]{16}$");
      let valid = exp.test(control.value);
      if (!valid)
        resolve({lenghtnot16: true });
      else
        resolve(null);
    });
  }

  static monthnotbetween1_12(control: AbstractControl): Promise<ValidationErrors | null> {
    return new Promise((resolve, reject) => {
      if (control.value < 1 || control.value>12 )
        resolve({ monthnotbetween1_12: true });
      else
        resolve(null);
    });
  }

  static lenghtnot3(control: AbstractControl): Promise<ValidationErrors | null> {
    return new Promise((resolve, reject) => {
      if (!new RegExp("^[0-9]{3}$").test(control.value))
        resolve({ lenghtnot3: true });
      else
        resolve(null);
    });
  }
  static nonexpiredcard(control: AbstractControl): Promise<ValidationErrors | null> {
    return new Promise((resolve, reject) => {
      let monthStr = control.parent.get('expMonth').value;
      let yearStr = control.value;

      let cardDate = new Date(yearStr + '-' + monthStr + '-01');

      //begin finding the last valid date for this card
      var d = new Date(cardDate);
      var years = Math.floor(1 / 12);
      var months = 1 - (years * 12);
      if (years) d.setFullYear(d.getFullYear() + years);
      if (months) d.setMonth(d.getMonth() + months);
      let lastvalidDate = d;
      //end finding Last Valid date

      let today = new Date();
      if (today > lastvalidDate)
        resolve({ nonexpiredcard: true });
      else
        resolve(null);
    });
  }

}
