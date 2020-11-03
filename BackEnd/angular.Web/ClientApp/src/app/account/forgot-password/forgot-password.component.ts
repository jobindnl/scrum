import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../account.service';
import { IForgotPassword } from './forgot-password';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  constructor(private fb: FormBuilder,
    private accountService: AccountService,
    private router: Router) { }
  formGroup: FormGroup;

  ngOnInit() {
    this.formGroup = this.fb.group({
      email: '',
      password: '',
    });
  }

  save() {
    let forgotPassword: IForgotPassword = Object.assign({}, this.formGroup.value);
    console.table(forgotPassword);

    this.accountService.forgotPassword(forgotPassword)
      .subscribe(forgotPassword => this.onSaveSuccess(),
        error => this.handleError(error));
  }

  handleError(error) {
    if (error && error.error) {
      alert(error.error["errors"]);
    }
  }

  onSaveSuccess() {
    this.router.navigate(["/email-confirm-sent"]);
  }

}
