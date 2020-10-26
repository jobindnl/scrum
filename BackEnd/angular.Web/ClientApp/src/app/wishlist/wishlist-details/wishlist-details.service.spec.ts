import { TestBed } from '@angular/core/testing';

import { WishlistDetailsService } from './wishlist-details.service';

describe('WishlistDetailsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: WishlistDetailsService = TestBed.get(WishlistDetailsService);
    expect(service).toBeTruthy();
  });
});
