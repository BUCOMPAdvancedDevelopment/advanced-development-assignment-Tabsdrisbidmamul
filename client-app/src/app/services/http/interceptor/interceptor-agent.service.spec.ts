import { TestBed } from '@angular/core/testing';

import { InterceptorAgentService } from './interceptor-agent.service';

describe('InterceptorAgentService', () => {
  let service: InterceptorAgentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InterceptorAgentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
