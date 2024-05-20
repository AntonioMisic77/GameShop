import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { ReceiptsMasterDetailComponent } from './components/receipts-master-detail/receipts-master-detail.component';
import { ArticlesComponent } from './components/articles/articles/articles.component';

export const routes: Routes = [{ path: '', component: AppComponent }, 
{ path: 'receipts', component: ReceiptsMasterDetailComponent },
{ path: 'articles', component: ArticlesComponent },
];
