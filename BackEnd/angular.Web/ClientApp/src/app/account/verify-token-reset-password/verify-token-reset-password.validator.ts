import { AbstractControl, ValidationErrors } from '@angular/forms';

export class VerifyTokenResetPasswordValidators {

  static matchPwds(control: AbstractControl) {
    let newPwd2 = control.get('newPwd');
    let confirmPwd2 = control.get('confirmPwd');
    if (newPwd2.value !== confirmPwd2.value) {
      return { pwdsDontMatch: true };
    }
    return null;
  }

  static samePwds(control: AbstractControl): Promise<ValidationErrors | null> {
    return new Promise((resolve, reject) => {
      let currentpassword2 = control.parent.get('currentpassword');
      if (currentpassword2.value == control.value)
        resolve({ samePwds: true });
      else
        resolve(null);
    });
  }

}
