import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../account/account.service';
import { IVerifyTokenResetPassword } from './verify-token-reset-password';
import { VerifyTokenResetPasswordValidators } from './verify-token-reset-password.validator';

@Component({
  selector: 'app-pwd-change',
  templateUrl: './verify-token-reset-password.component.html',
  styleUrls: ['./verify-token-reset-password.component.css']
})
export class VerifyTokenResetPasswordComponent implements OnInit {

  formGroup: FormGroup;
  token: string;
  email: string;

  constructor(private fb: FormBuilder,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private accountService: AccountService
  ) { }

  ngOnInit() {
    this.formGroup = this.fb.group({
      newPwd: [''],
      confirmPwd: ['', Validators.required]
    }, {
        validator: VerifyTokenResetPasswordValidators.matchPwds
    });

    this.activatedRoute.queryParams.subscribe(params => {
      if (params["token"] == undefined || params["email"] == undefined) {
        return;
      }
      this.token = params["token"];
      this.email = params["email"];
    });
  }

  save() {
    let resetPassword: IVerifyTokenResetPassword = Object.assign({}, this.formGroup.value);
    resetPassword.token = this.token;
    resetPassword.email = this.email;
    console.table(resetPassword);

    this.accountService.verifyTokenResetPassword(resetPassword)
      .subscribe(userProfile => this.onSaveSuccess(),
        error => this.handleError(error));
  }


  onSaveSuccess() {
    alert("Password changed sucssesfully");
    this.accountService.getToken();
    this.router.navigate(["/user-profile"]);
  }

  handleError(error) {
    if (error && error.error) {
      alert(error.error["errors"]);
    }
  }


}
