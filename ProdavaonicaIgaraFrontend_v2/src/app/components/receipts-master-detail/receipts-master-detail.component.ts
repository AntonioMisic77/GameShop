import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ReceiptService } from '../../services/receiptService';
import { QueryParametars } from '../../pageing/QueryParametars.model';
import { UserService } from '../../services/userService';
import { PagedResult } from '../../pageing/PagedResult.model';
import { UserDto } from '../../models/UserDto.model';
import { ReceiptDto } from '../../models/ReceiptDto.model';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReceiptItemDto } from '../../models/ReceiptItemDto.model';
import { ReceiptItemService } from '../../services/receiptItemService';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { CreateNewItemDialogComponent } from '../create-new-item-dialog/create-new-item-dialog.component';

@Component({
  selector: 'app-receipts-master-detail',
  templateUrl: './receipts-master-detail.component.html',
  styleUrl: './receipts-master-detail.component.css',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule, CommonModule, MatDialogModule ]
})
export class ReceiptsMasterDetailComponent {
  receipts: any[] = [];
  selectedReceipt: any = null;
  receiptForm: FormGroup;

  users: UserDto[] = [];
  isLoading = true;
  isDeleting = false;

  queryParametars: QueryParametars;
  totalCount: number = 0;
  pageSize: number = 1;
  pagedResult: PagedResult<ReceiptDto> | undefined

  _filterText: string = '';
  selectedUserId: any;
  selectedUser: UserDto | undefined;

  constructor(private receiptService: ReceiptService,
    private userService: UserService,
    private receiptItemService: ReceiptItemService,
    private fb: FormBuilder,
    private dialog: MatDialog) {
    this.queryParametars = new QueryParametars(0, 1, '', 1);
    {
      this.receiptForm = this.fb.group({
        id: Int16Array,
        cashierId: Int16Array,
        paymentMethod: '',
        companyId: Int16Array,
      });
    }
  }

  ngOnInit(): void {
    this.loadUsers();
    this.loadReceipts();
    this.setupFormChanges()
  }

  loadUsers() {
    this.userService.getUsers().subscribe(
      (data: UserDto[]) => {
        this.users = data;
        console.log(this.users)
      },
      (error: any) => {
        console.error('Error loading users:', error);
      }
    );
  }

  loadReceipts() {
    console.log(this.queryParametars)
    
    this.receiptService.getReceipts(this.queryParametars).subscribe(
      (result: PagedResult<ReceiptDto>) => {
        this.pagedResult = result;
        this.totalCount = this.pagedResult.totalCount;
        console.log(this.pagedResult.items)
        if (this.pagedResult.items.length > 0) {
          this.selectedReceipt = this.pagedResult.items[0];
          console.log(this.pagedResult.items[0])
          this.populateFormWithSelectedReceipt();
        }
      },
      (error: any) => {
        console.error('Error loading receipts:', error);
        this.isLoading = false;
      }
    );

  }

  setupFormChanges() {
    this.receiptForm.valueChanges.subscribe(value => {
      this.receiptForm.value.paymentMethod = value.paymentMethod
    });
  }

  populateFormWithSelectedReceipt() {
    if (this.selectedReceipt) {
      this.selectedUserId = this.selectedReceipt.cashierId
      this.receiptForm.patchValue({
        id: this.selectedReceipt.id,
        cashierId: this.selectedReceipt.cashierId,
        paymentMethod: this.selectedReceipt.paymentMethod,
        companyId: this.selectedReceipt.companyId,
      });
    }
  }
  selectReceipt(receipt: any) {
    this.selectedReceipt = receipt;
    this.populateFormWithSelectedReceipt();
  }

  saveReceipt() {
    const updatedReceiptData = this.receiptForm.value;
    console.log(this.receiptForm.value)

    this.receiptService.updateReceipt(updatedReceiptData).subscribe(
      (result: any) => {
        console.log("Uspjesno spremljeno")
      },
      (error: any) => {
        console.error('Error updating receipt:', error);
      }
    );
  }


  deleteReceipt() {
    this.receiptService.deleteReceipt(this.selectedReceipt).subscribe(
      (result: any) => {
        this.loadReceipts()
      },
      (error: any) => {
        console.error('Error deleting receipt:', error);
      }
    );
    console.log('Deleted receipt:', this.selectedReceipt);
    this.isDeleting = true;
  }

  nextPage() {
    this.queryParametars.startIndex = this.pageSize * this.queryParametars.pageNumber;
    this.queryParametars.pageNumber += 1;
    this.isLoading = true;
    this.loadReceipts()
  }

  previousPage() {
    this.queryParametars.startIndex -= this.pageSize;
    this.queryParametars.pageNumber -= 1;
    this.isLoading = true;
    this.loadReceipts()
  }

  pageCount(): number {
    return Math.ceil(this.totalCount / this.pageSize)
  }

  onChangeUser(event: any) {
    const selectedUserId = event.target.value;
    this.selectedUser = this.users.find(user => user.id === parseInt(selectedUserId));
    console.log(this.selectedUser)
    this.receiptForm.patchValue({
      cashier: this.selectedUser
    });
  }

  editItem(item:any){

  }

  deleteItem(item: ReceiptItemDto) {
    if (!this.selectedReceipt) return;
    const receiptItemId = item.id; 
    console.log(receiptItemId)
    this.receiptItemService.deleteReceiptItem(receiptItemId).subscribe(
      (result: any) => {
        const index = this.selectedReceipt!.receiptItems.indexOf(item);
        if (index > -1) {
          this.selectedReceipt!.receiptItems.splice(index, 1);
          this.populateFormWithSelectedReceipt(); 
        }
      },
      (error: any) => {
        console.error('Error deleting item:', error);
      }
    );
  }

   createNewItem() {
    const dialogRef = this.dialog.open(CreateNewItemDialogComponent, {
      width: '400px',
      data: {selectedReceipt: this.selectedReceipt} // Možete proslijediti podatke ako je potrebno
    });

    dialogRef.afterClosed().subscribe((result:any) => {
      console.log(result)
      if(result != undefined){
        this.selectedReceipt.receiptItems.push(result)
      }
    });
  }

}