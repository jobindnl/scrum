import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IUserProfile } from '../user-profile';
import { UserProfileService } from '../user-profile.service';


@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile-form.component.html',
  styleUrls: ['./user-profile-form.component.css']
})
export class UserProfileFormComponent implements OnInit {
  userProfile: IUserProfile;

  constructor(private fb: FormBuilder,
    private userProfileService: UserProfileService,
    private router: Router,
    private activatedRoute: ActivatedRoute) { }


  editMode: boolean = false;
  formGroup: FormGroup;
  userProfileId: number;


  ngOnInit() {
    this.formGroup = this.fb.group({
      name: '',
      nickName: '',
      email: '',
      homeAddressId: 0,
      defaultCreditCardId: 0
    });

    this.activatedRoute.params.subscribe(params => {
      this.editMode = true;
      this.userProfileId = 1;
      this.userProfileService.getUserProfile(this.userProfileId.toString())
        .subscribe(userProfileFromApi => this.loadForm(userProfileFromApi),
          error => console.error(error));
    });
  }

  loadForm(userProfile: IUserProfile) {
    this.userProfile = userProfile;
    this.formGroup.patchValue({
      name: userProfile.name,
      nickName: userProfile.nickName,
      email: userProfile.email,
      homeAddressId: userProfile.homeAddressId,
      defaultCreditCardId: userProfile.defaultCreditCardId
    })
  }

  save() {
    let userProfile: IUserProfile = Object.assign({}, this.formGroup.value);
    console.table(userProfile);

    var x: number = +this.userProfileId;
    userProfile.id = x;
    this.userProfileService.updateUserProfile(userProfile)
      .subscribe(userProfile => this.onSaveSuccess(),
        error => console.error(error));
  }

  onSaveSuccess() {
    this.router.navigate(["/user-profile"]);
  }
  
}
