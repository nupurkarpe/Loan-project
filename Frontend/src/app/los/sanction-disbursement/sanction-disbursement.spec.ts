import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SanctionDisbursement } from './sanction-disbursement';

describe('SanctionDisbursement', () => {
  let component: SanctionDisbursement;
  let fixture: ComponentFixture<SanctionDisbursement>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SanctionDisbursement]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SanctionDisbursement);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
