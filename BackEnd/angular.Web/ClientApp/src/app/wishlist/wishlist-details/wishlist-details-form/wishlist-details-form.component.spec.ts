import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WishlistDetailsFormComponent } from './wishlist-details-form.component';

describe('WishlistDetailsFormComponent', () => {
  let component: WishlistDetailsFormComponent;
  let fixture: ComponentFixture<WishlistDetailsFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WishlistDetailsFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WishlistDetailsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
