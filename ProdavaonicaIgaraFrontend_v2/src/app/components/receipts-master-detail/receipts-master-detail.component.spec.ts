import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReceiptsMasterDetailComponent } from './receipts-master-detail.component';

describe('ReceiptsMasterDetailComponent', () => {
  let component: ReceiptsMasterDetailComponent;
  let fixture: ComponentFixture<ReceiptsMasterDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReceiptsMasterDetailComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReceiptsMasterDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
