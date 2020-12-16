import { TestBed } from '@angular/core/testing';

import { OptionsInfoService } from './options-info.service';

describe('OptionsInfoService', () => {
  let service: OptionsInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OptionsInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
