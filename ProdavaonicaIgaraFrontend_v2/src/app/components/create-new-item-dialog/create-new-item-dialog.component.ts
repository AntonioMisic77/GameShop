import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { ArticleService } from '../../services/articleService';
import { QueryParametars } from '../../pageing/QueryParametars.model';
import { ArticleDto } from '../../models/ArticleDto.model';
import { ReceiptItemService } from '../../services/receiptItemService';
import { PagedResult } from '../../pageing/PagedResult.model';

@Component({
  selector: 'app-create-new-item-dialog',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, MatDialogModule, FormsModule ],
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
    private receiptItemService: ReceiptItemService
  ) { this.queryParametars = new QueryParametars(0, 1, " ", 10);
  this.queryParametars.filterText = " "
  this.newItemForm = this.formBuilder.group({
    id: Int16Array,
    quantity: Int16Array,
    receiptId: this.selectedReceipt.selectedReceipt.id,
    articleId: Int16Array
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
    if (this.newItemForm?.valid) {
      const newItemData = this.newItemForm.value;
      console.log(newItemData)
      // Ovdje možete dodati logiku za spremanje novog itema
      this.receiptItemService.createReceiptItem(newItemData).subscribe((result:any)=>{
        this.dialogRef.close(result)
      },
      (error: any) => {
        console.error('Error creating receiptItem:', error);
      }
    );
       // Proslijedi podatke ako je potrebno
    }
  }
}