import { TestBed } from '@angular/core/testing';

import { OrderClientService } from './orderClient.service';

describe('OrderService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: OrderClientService = TestBed.get(OrderService);
    expect(service).toBeTruthy();
  });
});
