import { Component, OnInit } from '@angular/core';
import { ArticleService } from '../../../services/articleService';
import { QueryParametars } from '../../../pageing/QueryParametars.model';
import { AbstractControl, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { SupplierService } from '../../../services/supplierService';

function nonNegativeValidator(control: AbstractControl): ValidationErrors | null {
  const value = control.value;
  return value < 0 ? { nonNegative: true } : null;
}

@Component({
  selector: 'app-articles',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule, CommonModule, MatDialogModule, MatSnackBarModule, FormsModule],
  templateUrl: './articles.component.html',
  styleUrl: './articles.component.css'
})
export class ArticlesComponent implements OnInit {
  articles: any[] = [];
  filteredArticles: any[] = [];
  searchQuery: string = '';
  totalCount: number = 0;
  pageSize: number = 10;
  queryParametars: QueryParametars;
  newArticleForm: FormGroup
  showNewArticleForm: boolean = false;
  showEditArticleForm: boolean = false;
  suppliers: any[] = [];

  constructor(private articleService: ArticleService,
    private supplierService: SupplierService,
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar
  ) {
    this.queryParametars = new QueryParametars(0, 1, " ", this.pageSize);
    this.newArticleForm = this.formBuilder.group({
      id: Int16Array,
      price: ['', [Validators.required, nonNegativeValidator]],
      description: ['', Validators.required],
      name: ['', [Validators.required, Validators.maxLength(100)]],
      stockQuantity: ['', [Validators.required, nonNegativeValidator]],
      supplierId: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadSuppliers();
    this.loadArticles();
    this.subscribeToFormChanges();
  }

  loadArticles(): void {
    this.articleService.getArticles(this.queryParametars).subscribe((data: any) => {
      this.articles = data.items;
      this.totalCount = data.totalCount;
    });
  }
  loadSuppliers(): void {
    this.supplierService.getSuppliers().subscribe((data: any) => {
      this.suppliers = data
      console.log(data)
    })
  }

  private subscribeToFormChanges(): void {
    this.newArticleForm.valueChanges.subscribe(value => {
      this.newArticleForm.value.supplierId = value.supplierId
      this.newArticleForm.value.price = value.price
      this.newArticleForm.value.description = value.description
      this.newArticleForm.value.name = value.name
      this.newArticleForm.value.stockQuantity = value.stockQuantity
      console.log(this.newArticleForm.value)
    });

  }

  createArticle(): void {
    console.log(this.newArticleForm.value)
    if (this.newArticleForm.valid) {
    this.articleService.createArticle(this.newArticleForm.value).subscribe(() => {
      this.snackBar.open('Article created successfully!', 'Close', {
        duration: 3000,
        verticalPosition: 'bottom',
        horizontalPosition: 'center'
      });
      this.newArticleForm.reset()
      this.showNewArticleForm = false;
      this.loadArticles();
    }, (error:any)=>{
      this.snackBar.open('Error while creating article', 'Close', {
        duration: 3000,
        verticalPosition: 'bottom',
        horizontalPosition: 'center'
      });
    });
    }else {
      this.snackBar.open('Please correct the errors in the form', 'Close', {
        duration: 3000,
        verticalPosition: 'bottom',
        horizontalPosition: 'center'
      });
    }
  }

  deleteArticle(id: number): void {
    this.articleService.deleteArticle(id).subscribe(() => {
      this.snackBar.open('Article deleted successfully!', 'Close', {
        duration: 3000,
        verticalPosition: 'bottom',
        horizontalPosition: 'center'
      });
      this.loadArticles();
    },(error:any)=>{
      this.snackBar.open(error.error.Message, 'Close', {
        duration: 3000,
        verticalPosition: 'bottom',
        horizontalPosition: 'center'
      });
    });
  }
  searchArticles(): void {
    this.queryParametars.filterText = this.searchQuery.toLocaleLowerCase()
    this.loadArticles()
  }

  editArticle(): void {
    console.log(this.newArticleForm.value)
    if (this.newArticleForm.valid) {
    this.articleService.updateArticle(this.newArticleForm.value).subscribe(() => {
      this.snackBar.open('Article updated successfully!', 'Close', {
        duration: 3000,
        verticalPosition: 'bottom',
        horizontalPosition: 'center'
      });
      this.newArticleForm.reset()
      this.showEditArticleForm = false;
      this.loadArticles();
    }, (error:any)=>{
      this.snackBar.open(error.error.Message, 'Close', {
        duration: 3000,
        verticalPosition: 'bottom',
        horizontalPosition: 'center'
      });
    });
  } else {
    this.snackBar.open('Please correct the errors in the form', 'Close', {
      duration: 3000,
      verticalPosition: 'bottom',
      horizontalPosition: 'center'
    });
  }
  }

  editArticleToggle(article:any) {
    this.newArticleForm.patchValue({
      id: article.id, // Ako je potrebno, postavite i ID
      name: article.name,
      description: article.description,
      price: article.price,
      supplierId: article.supplierId,
      stockQuantity: article.stockQuantity // Pretpostavljamo da artikl ima property supplierId
    });
    this.showEditArticleForm = !this.showEditArticleForm;
  }

  cancel(){
    this.showEditArticleForm = !this.showEditArticleForm;
    this.newArticleForm.reset()
  }

  toggleNewArticleForm(): void {
    this.showNewArticleForm = !this.showNewArticleForm;
  }

  nextPage() {
    this.queryParametars.startIndex = this.pageSize * this.queryParametars.pageNumber;
    this.queryParametars.pageNumber += 1;
    this.loadArticles()
  }

  previousPage() {
    this.queryParametars.startIndex -= this.pageSize;
    this.queryParametars.pageNumber -= 1;
    this.loadArticles()
  }

  pageCount(): number {
    return Math.ceil(this.totalCount / this.pageSize)
  }

}
