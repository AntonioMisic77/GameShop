import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.model';
import { ReceiptsMasterDetailComponent } from './components/receipts-master-detail/receipts-master-detail.component';

@NgModule({
  declarations: [
    ReceiptsMasterDetailComponent,
    AppComponent
    // Dodajte ostale komponente koje koristite u aplikaciji
  ],
  imports: [
    BrowserModule,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    AppRoutingModule
    // Uključivanje definiranih ruta
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
