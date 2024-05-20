import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { ArticleService } from '../../../services/articleService';
import { ReceiptItemService } from '../../../services/receiptItemService';
import { QueryParametars } from '../../../pageing/QueryParametars.model';
import { ArticleDto } from '../../../models/ArticleDto.model';
import { CommonModule } from '@angular/common';
import { PagedResult } from '../../../pageing/PagedResult.model';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

function nonNegativeValidator(control: AbstractControl): ValidationErrors | null {
  const value = control.value;
  return value < 0 ? { nonNegative: true } : null;
}


@Component({
  selector: 'app-edit-item-dialog',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, MatDialogModule, FormsModule, MatSnackBarModule],
  templateUrl: './edit-item-dialog.component.html',
  styleUrl: './edit-item-dialog.component.css'
})
export class EditItemDialogComponent implements OnInit {
  editItemForm: FormGroup;
  quantity: number | undefined;
  queryParametars: QueryParametars;
  articles: Array<ArticleDto> | undefined;
  articleId: number | undefined;
  constructor(
    public dialogRef: MatDialogRef<EditItemDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private formBuilder: FormBuilder,
    private articleService: ArticleService,
    private receiptItemService: ReceiptItemService,
    private snackBar: MatSnackBar
  ) {
    this.queryParametars = new QueryParametars(0, 1, " ", 10);
    this.editItemForm = this.formBuilder.group({
      id: this.data.id,
      quantity: ['', [Validators.required, nonNegativeValidator]],
      receiptId: this.data.receiptId,
      articleId: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.subscribeToFormChanges();
    console.log(this.data)
    this.loadArticles()

  }

  loadArticles() {
    console.log(this.queryParametars)

    this.articleService.getArticles(this.queryParametars).subscribe((result: PagedResult<ArticleDto>) => {
      this.articles = result.items
      console.log(result.items)
    },
      (error: any) => {
        console.error('Error loading articles:', error);
      }
    );
  }
  private subscribeToFormChanges(): void {
    this.editItemForm.valueChanges.subscribe(value => {
      this.editItemForm.value.quantity = value.quantity
      this.editItemForm.value.articleId = value.articleId
      console.log(this.editItemForm.value)
    });

  }

  onCancelClick(): void {
    this.dialogRef.close(); // Zatvara dijalog bez spremanja
  }

  onSaveClick(): void {
    console.log(this.editItemForm.valid)
    if (this.editItemForm.valid) {
      const editItemData = this.editItemForm.value;
      console.log(editItemData)
      this.receiptItemService.updateReceiptItem(editItemData).subscribe((result: any) => {
        this.snackBar.open('Receipt item updated successfully!', 'Close', {
          duration: 3000,
          verticalPosition: 'bottom',
          horizontalPosition: 'center'
        });
        this.dialogRef.close(result)
      },
        (error: any) => {
          console.log(error)
          this.snackBar.open(error.error.Message, 'Close', {
            duration: 3000,
            verticalPosition: 'bottom',
            horizontalPosition: 'center'
          });
        }
      );
    } else {
      this.snackBar.open('Please correct the errors in the form', 'Close', {
        duration: 3000,
        verticalPosition: 'bottom',
        horizontalPosition: 'center'
      });
    }
  }

}
