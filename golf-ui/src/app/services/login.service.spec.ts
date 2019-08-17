import { TestBed } from '@angular/core/testing';

import { Login } from './login.service';

describe('Login', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: Login = TestBed.get(Login);
    expect(service).toBeTruthy();
  });
});
