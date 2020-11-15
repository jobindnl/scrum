import { TestBed } from '@angular/core/testing';

import { LogInterceptorService } from './log-interceptor.service';

describe('LogInterceptorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LogInterceptorService = TestBed.get(LogInterceptorService);
    expect(service).toBeTruthy();
  });
});
