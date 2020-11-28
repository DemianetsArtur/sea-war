import { TestBed } from '@angular/core/testing';

import { HandlerAlertService } from './handler-alert.service';

describe('HandlerAlertService', () => {
  let service: HandlerAlertService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HandlerAlertService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
