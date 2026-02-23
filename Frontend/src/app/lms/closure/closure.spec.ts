import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Closure } from './closure';

describe('Closure', () => {
  let component: Closure;
  let fixture: ComponentFixture<Closure>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Closure]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Closure);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
