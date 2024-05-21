import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateNewItemDialogComponent } from './create-new-item-dialog.component';

describe('CreateNewItemDialogComponent', () => {
  let component: CreateNewItemDialogComponent;
  let fixture: ComponentFixture<CreateNewItemDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateNewItemDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateNewItemDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
