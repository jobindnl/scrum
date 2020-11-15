import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { VerifyTokenResetPasswordComponent } from './verify-token-reset-password.component';

describe('PwdChangeComponent', () => {
  let component: VerifyTokenResetPasswordComponent;
  let fixture: ComponentFixture<VerifyTokenResetPasswordComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [VerifyTokenResetPasswordComponent]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VerifyTokenResetPasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
