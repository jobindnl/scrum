import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PwdChangeComponent } from './pwd-change.component';

describe('PwdChangeComponent', () => {
  let component: PwdChangeComponent;
  let fixture: ComponentFixture<PwdChangeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PwdChangeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PwdChangeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
