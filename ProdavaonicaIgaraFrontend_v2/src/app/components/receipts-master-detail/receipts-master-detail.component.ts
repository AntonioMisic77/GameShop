import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
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
import { EditItemDialogComponent } from '../edit-item-dialog/edit-item-dialog/edit-item-dialog.component';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-receipts-master-detail',
  templateUrl: './receipts-master-detail.component.html',
  styleUrl: './receipts-master-detail.component.css',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule, CommonModule, MatDialogModule, MatSnackBarModule ]
})
export class ReceiptsMasterDetailComponent {
  receipts: any[] = [];
  selectedReceipt: any = null;
  receiptForm: FormGroup;
  newReceiptForm: FormGroup;

  users: UserDto[] = [];
  isLoading = true;
  isDeleting = false;
  isCreatingNewReceipt = false;

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
    private dialog: MatDialog,
    private snackBar: MatSnackBar) {
    this.queryParametars = new QueryParametars(0, 1, '', 1);
    {
      this.receiptForm = this.fb.group({
        id: Int16Array,
        cashierId: Int16Array,
        paymentMethod: '',
        companyId: Int16Array,
      });
    }
    {
      this.newReceiptForm = this.fb.group({
        id: Int16Array,
        cashierId: ['', Validators.required],
        paymentMethod: ['', Validators.required],
        companyId: 1,
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
      console.log(this.receiptForm.value)
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
        this.snackBar.open('Receipt saved successfully!', 'Close', {
          duration: 3000,
          verticalPosition: 'bottom',
          horizontalPosition: 'center'
        });
      },
      (error: any) => {
        console.error('Error updating receipt:', error);
        this.snackBar.open(error.error.Message, 'Close', {
          duration: 3000,
          verticalPosition: 'bottom'
        });
      })
  }

  createNewReceipt(){
    const newReceiptData = this.newReceiptForm.value
    console.log(newReceiptData)
    this.receiptService.createReceipt(newReceiptData).subscribe(
      (result: any) => {
        this.snackBar.open('Receipt created successfully!', 'Close', {
          duration: 3000,
          verticalPosition: 'bottom',
          horizontalPosition: 'center'
        });
        this.newReceiptForm.reset()
        this.isCreatingNewReceipt = false;
        this.loadReceipts()
      },
      (error: any) => {
        this.snackBar.open(error.error.Message, 'Close', {
          duration: 3000,
          verticalPosition: 'bottom',
          horizontalPosition: 'center'
        });
      }
    );
  }

  deleteReceipt() {
    this.receiptService.deleteReceipt(this.selectedReceipt.id).subscribe(
      (result: any) => {
        this.snackBar.open('Receipt deleted successfully!', 'Close', {
          duration: 3000,
          verticalPosition: 'bottom',
          horizontalPosition: 'center'
        });
        this.queryParametars = new QueryParametars(0, 1, '', 1);
        this.loadReceipts()
      },
      (error: any) => {
        this.snackBar.open(error.error.Message, 'Close', {
          duration: 3000,
          verticalPosition: 'bottom',
          horizontalPosition: 'center'
        });
      }
    );
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

  onChangeUserWhenCreating(event: any) {
    const selectedUserId = event.target.value;
    this.selectedUser = this.users.find(user => user.id === parseInt(selectedUserId));
    console.log(this.selectedUser)
    this.newReceiptForm.patchValue({
      cashier: this.selectedUser
    });
  }

  toggleNewReceiptForm(): void {
    this.isCreatingNewReceipt = !this.isCreatingNewReceipt;
    this.newReceiptForm.reset()
  }

  editItem(item:any){
    console.log(item)
    const dialogRef = this.dialog.open(EditItemDialogComponent, {
      width: '400px',
      data: item // Možete proslijediti podatke ako je potrebno
    });

    dialogRef.afterClosed().subscribe((result:any) => {
      console.log(result)
      if(result != undefined){
        this.loadReceipts()
      }
    });
  }

  deleteItem(item: ReceiptItemDto) {
    if (!this.selectedReceipt) return;
    const receiptItemId = item.id; 
    console.log(receiptItemId)
    this.receiptItemService.deleteReceiptItem(receiptItemId).subscribe(
      (result: any) => {
        this.snackBar.open('Receipt item deleted successfully!', 'Close', {
          duration: 3000,
          verticalPosition: 'bottom',
          horizontalPosition: 'center'
        });
        const index = this.selectedReceipt!.receiptItems.indexOf(item);
        if (index > -1) {
          this.selectedReceipt!.receiptItems.splice(index, 1);
          this.populateFormWithSelectedReceipt(); 
        }
      },
      (error: any) => {
        this.snackBar.open(error.error.Message, 'Close', {
          duration: 3000,
          verticalPosition: 'bottom',
          horizontalPosition: 'center'
        });
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