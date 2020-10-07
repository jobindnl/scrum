import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IBook } from '../book/book';
import { IUserProfile } from './user-profile';
import { UserProfileService } from './user-profile.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  public userProfile: IUserProfile;
  public formGroup: FormGroup;
  public userProfileId: number;

  constructor(private fb: FormBuilder,
    private userProfileService: UserProfileService,
    private router: Router) { }

  ngOnInit() {
    this.formGroup = this.fb.group({
      name: '',
      nickName: '',
      email: '',
      homeAddressId: '',
    });
    this.userProfileId = 1;
    this.userProfileService.getUserProfile(this.userProfileId.toString())
      .subscribe(userProfilefromapi => this.loadData(userProfilefromapi),
        error => console.error(error));
  }

  loadData(userProfile: IUserProfile) {
    this.userProfile = userProfile;
    this.formGroup.patchValue({
      name: userProfile.name,
      nickName: userProfile.nickName,
      email: userProfile.email,
      homeAddressId: userProfile.homeAddressId,
    });
  }

}
