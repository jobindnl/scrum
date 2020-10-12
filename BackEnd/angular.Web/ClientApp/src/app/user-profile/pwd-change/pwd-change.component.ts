import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { OldPwdValidators } from './old-pwd.validator';

@Component({
  selector: 'app-pwd-change',
  templateUrl: './pwd-change.component.html',
  styleUrls: ['./pwd-change.component.css']
})
export class PwdChangeComponent implements OnInit {

  formGroup: FormGroup;

  constructor(private fb: FormBuilder,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.formGroup = this.fb.group({
      oldPwd: ['', Validators.required, OldPwdValidators.shouldBe1234],
      newPwd: ['', Validators.required],
      confirmPwd: ['', Validators.required]
    }, {
      validator: OldPwdValidators.matchPwds
    });

  }

  get oldPwd() {
    return this.formGroup.get('oldPwd');
  }

  get newPwd() {
    return this.formGroup.get('newPwd');
  }

  get confirmPwd() {
    return this.formGroup.get('confirmPwd');
  }

}
