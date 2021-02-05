import { TestBed } from '@angular/core/testing';

import { HubInfoService } from './hub-info.service';

describe('HubInfoService', () => {
  let service: HubInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HubInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
