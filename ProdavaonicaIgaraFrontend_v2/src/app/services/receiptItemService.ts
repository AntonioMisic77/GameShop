import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environment';

@Injectable({
  providedIn: 'root'
})
export class ReceiptItemService {
  private apiUrl = environment.apiUrl; // Zamijeni s URL-om tvog ASP.NET API-ja

  constructor(private http: HttpClient) { }

  getReceiptItem(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/ReceiptItem/${id}`).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  getReceiptItems(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/ReceiptItem`).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  createReceiptItem(receiptItem: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/ReceiptItem`, receiptItem).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  updateReceiptItem(receiptItem: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/ReceiptItem/`, receiptItem).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  deleteReceiptItem(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/ReceiptItem/${id}`).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }
}
