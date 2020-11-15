import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../account/account.service';
import { OldPwdValidators } from './old-pwd.validator';
import { IPwdChange } from './pwdchange';

@Component({
  selector: 'app-pwd-change',
  templateUrl: './pwd-change.component.html',
  styleUrls: ['./pwd-change.component.css']
})
export class PwdChangeComponent implements OnInit {

  formGroup: FormGroup;

  constructor(private fb: FormBuilder,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private accountService: AccountService
  ) { }

  ngOnInit() {
    this.formGroup = this.fb.group({
      currentpassword: ['', Validators.required],
      newPwd: ['', Validators.required, OldPwdValidators.samePwds],
      confirmPwd: ['', Validators.required]
    }, {
      validator: OldPwdValidators.matchPwds
    });

  }

  save() {
    let pwdChange: IPwdChange = Object.assign({}, this.formGroup.value);
    console.table(pwdChange);

    this.accountService.changePassword(pwdChange)
      .subscribe(userProfile => this.onSaveSuccess(),
        error => this.handleError(error));
  }

  onSaveSuccess() {
    alert("Password changed sucssesfully");
    this.router.navigate(["/user-profile"]);
  }

  handleError(error) {
    if (error && error.error) {
      alert(error.error["errors"]);
    }
  }


}
