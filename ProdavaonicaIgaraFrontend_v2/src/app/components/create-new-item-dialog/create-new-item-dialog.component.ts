import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { ArticleService } from '../../services/articleService';
import { QueryParametars } from '../../pageing/QueryParametars.model';
import { ArticleDto } from '../../models/ArticleDto.model';
import { ReceiptItemService } from '../../services/receiptItemService';
import { PagedResult } from '../../pageing/PagedResult.model';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

function nonNegativeValidator(control: AbstractControl): ValidationErrors | null {
  const value = control.value;
  return value < 0 ? { nonNegative: true } : null;
}


@Component({
  selector: 'app-create-new-item-dialog',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, MatDialogModule, FormsModule, MatSnackBarModule ],
  templateUrl: './create-new-item-dialog.component.html',
  styleUrl: './create-new-item-dialog.component.css'
})
export class CreateNewItemDialogComponent implements OnInit {
  newItemForm: FormGroup;
  quantity: number | undefined;
  queryParametars: QueryParametars;
  articles: Array<ArticleDto> | undefined;
  articleId: number | undefined;
  constructor(
    public dialogRef: MatDialogRef<CreateNewItemDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public selectedReceipt: any,
    private formBuilder: FormBuilder,
    private articleService: ArticleService,
    private receiptItemService: ReceiptItemService,
    private snackBar: MatSnackBar
  ) { this.queryParametars = new QueryParametars(0, 1, " ", 10);
  this.newItemForm = this.formBuilder.group({
    id: Int16Array,
    quantity: ['', [Validators.required, nonNegativeValidator]],
    receiptId: this.selectedReceipt.selectedReceipt.id,
    articleId:  ['', Validators.required]
  });
  }

  ngOnInit(): void {
    this.subscribeToFormChanges();
    console.log(this.queryParametars)
    this.loadArticles()
    
  }

  loadArticles() {
    console.log(this.queryParametars)

    this.articleService.getArticles(this.queryParametars).subscribe((result:PagedResult<ArticleDto>)=>{
      this.articles = result.items  
      console.log(result.items)
    },
    (error: any) => {
      console.error('Error loading articles:', error);
    }
  );
  }

  private subscribeToFormChanges(): void {
    this.newItemForm.valueChanges.subscribe(value => {
      this.newItemForm.value.quantity = value.quantity
      this.newItemForm.value.articleId = value.articleId
      console.log(this.newItemForm.value)
    });
    
    }
  
  onCancelClick(): void {
    this.dialogRef.close(); // Zatvara dijalog bez spremanja
  }

  onSaveClick(): void {
    if (this.newItemForm.valid) {
      const newItemData = this.newItemForm.value;
      console.log(newItemData)
      // Ovdje možete dodati logiku za spremanje novog itema
      this.receiptItemService.createReceiptItem(newItemData).subscribe((result:any)=>{
        this.snackBar.open('Receipt item created successfully!', 'Close', {
          duration: 3000,
          verticalPosition: 'bottom',
          horizontalPosition: 'center'
        });
        this.dialogRef.close(result)
      },
      (error: any) => {
        this.snackBar.open(error.error.Message, 'Close', {
          duration: 3000,
          verticalPosition: 'bottom',
          horizontalPosition: 'center'
        });
      }
    );
    } else {
      console.log("inace")
      this.snackBar.open('Please correct the errors in the form', 'Close', {
        duration: 3000,
        verticalPosition: 'bottom',
        horizontalPosition: 'center'
      });
    }
  }
}