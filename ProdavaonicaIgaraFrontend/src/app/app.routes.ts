import { Routes } from '@angular/router';
import { ReceiptsMasterDetailComponent } from './components/receipts-master-detail/receipts-master-detail.component';
import { AppComponent } from './app.component';

export const routes: Routes = [
  { path: '', component: AppComponent }, 
  { path: 'receipts', component: ReceiptsMasterDetailComponent },// postavljanje ReceiptsMasterDetailComponent kao početne stranice
  // Ovdje možete dodati ostale rute prema potrebi vaše aplikacije
];
