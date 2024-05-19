import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { ReceiptsMasterDetailComponent } from './components/receipts-master-detail/receipts-master-detail.component';

const routes: Routes = [
    { path: '', component: AppComponent },
    { path: 'receipts', component: ReceiptsMasterDetailComponent },
    // Dodajte ostale rute ovisno o potrebama vaše aplikacije
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
