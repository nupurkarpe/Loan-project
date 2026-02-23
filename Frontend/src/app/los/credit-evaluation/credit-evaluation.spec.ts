import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreditEvaluation } from './credit-evaluation';

describe('CreditEvaluation', () => {
  let component: CreditEvaluation;
  let fixture: ComponentFixture<CreditEvaluation>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreditEvaluation]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreditEvaluation);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
