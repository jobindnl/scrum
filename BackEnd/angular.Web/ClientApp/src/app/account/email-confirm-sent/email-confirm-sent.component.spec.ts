import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmailConfirmSentComponent } from './email-confirm-sent.component';

describe('EmailConfirmSentComponent', () => {
  let component: EmailConfirmSentComponent;
  let fixture: ComponentFixture<EmailConfirmSentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmailConfirmSentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmailConfirmSentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
