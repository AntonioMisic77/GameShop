import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ReceiptItemService {
  private apiUrl = 'https://localhost:7174'; // Zamijeni s URL-om tvog ASP.NET API-ja

  constructor(private http: HttpClient) { }

  getReceiptItem(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/receipt-items/${id}`).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  getReceiptItems(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/receipt-items`).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  createReceiptItem(receiptItem: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/api/ReceiptItem`, receiptItem).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  updateReceiptItem(receiptItem: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/api/ReceiptItem/`, receiptItem).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  deleteReceiptItem(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/api/ReceiptItem/${id}`).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }
}
