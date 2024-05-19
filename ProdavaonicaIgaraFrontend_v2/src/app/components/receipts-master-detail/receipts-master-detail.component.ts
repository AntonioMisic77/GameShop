import { Component } from '@angular/core';
import { FormBuilder,FormControl,  FormGroup, ReactiveFormsModule } from '@angular/forms';  
import { ReceiptService } from '../../services/receiptService';
import { QueryParametars } from '../../pageing/QueryParametars.model';
import { UserService } from '../../services/userService';
import { PagedResult } from '../../pageing/PagedResult.model';
import { UserDto } from '../../models/UserDto.model';
import { ReceiptDto } from '../../models/ReceiptDto.model';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CompanyDto } from '../../models/CompanyDto.model';
import { ReceiptItemDto } from '../../models/ReceiptItemDto.model';

@Component({
  selector: 'app-receipts-master-detail',
  templateUrl: './receipts-master-detail.component.html',
  styleUrl: './receipts-master-detail.component.css',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule, CommonModule]
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
    private fb: FormBuilder) {
    this.queryParametars = new QueryParametars(0, 1, '');
    {
      this.receiptForm = this.fb.group({
        cashierId: Int16Array,
        paymentMethod: '',
        companyId: Int16Array,
        cashier: UserDto,
        company: CompanyDto,
        receiptItems: Array(ReceiptItemDto)
      });
    }
  }

  ngOnInit(): void {
    this.loadUsers();
    this.loadReceipts();
  }

  loadUsers() {
    this.userService.getUsers().subscribe(
      (data: UserDto[]) => {
        this.users = data;
        console.log(this.users    )
      },
      (error: any) => {
        console.error('Error loading users:', error);
      }
    );
  }

  loadReceipts() {
    this.receiptService.getReceipts(this.queryParametars).subscribe(
      (result: PagedResult<ReceiptDto>) => {
        this.pagedResult = result;
        this.totalCount = this.pagedResult.totalCount;
         // Postavljanje prvog računa kao odabranog
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

  populateFormWithSelectedReceipt() {
    if (this.selectedReceipt) {
      this.selectedUser = this.selectedReceipt.cashier
      console.log(this.selectedUser)
      this.receiptForm.patchValue({
        cashierId: this.selectedReceipt.cashierId,
        paymentMethod: this.selectedReceipt.paymentMethod,
        companyId: this.selectedReceipt.companyId,
        cashier: this.selectedReceipt.cashier,
        receiptItems: this.selectedReceipt.receiptItems
      });
    }
  }
  selectReceipt(receipt: any) {
    this.selectedReceipt = receipt;
    this.populateFormWithSelectedReceipt();
  }  

  saveReceipt() {
    // Prvo dohvatite vrednosti iz forme kako biste ažurirali račun
    const updatedReceiptData = this.receiptForm.value;
    console.log(this.receiptForm.value)
  
    // Pozovite servis za ažuriranje računa sa novim podacima
    this.receiptService.updateReceipt(updatedReceiptData).subscribe(
      (result: any) => {
        // Ako je ažuriranje uspešno, možete osvežiti listu računa
        this.loadReceipts();
        this.selectedReceipt = result
        this.populateFormWithSelectedReceipt();
        // Možete takođe postaviti prvi ažurirani račun kao trenutno izabran

      },
      (error: any) => {
        console.error('Error updating receipt:', error);
        // Ovde možete obraditi grešku na odgovarajući način
      }
    );
  }
  

  deleteReceipt() {
    // Implementiraj logiku za brisanje računa
    console.log('Deleted receipt:', this.selectedReceipt);
    this.isDeleting = true;
    // Pozovi metodu za brisanje iz servisa
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
}