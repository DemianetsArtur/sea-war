import { TestBed } from '@angular/core/testing';

import { NotificationInfoService } from './notification-info.service';

describe('NotificationInfoService', () => {
  let service: NotificationInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NotificationInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
