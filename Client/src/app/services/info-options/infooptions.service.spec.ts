import { TestBed } from '@angular/core/testing';
import { InfoOptionsService } from './infooptions.service';


describe('InfooptionsService', () => {
  let service: InfoOptionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InfoOptionsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
