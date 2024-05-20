import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ReceiptService {
  private apiUrl = 'https://localhost:7174'; // Zamijeni s URL-om tvog ASP.NET API-ja

  constructor(private http: HttpClient) { }

  getReceipt(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/api/receipts/${id}`).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  getReceipts(params: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/api/receipt/paged`,  params ).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  createReceipt(receipt: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/api/receipt`, receipt).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  updateReceipt(receipt: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/api/receipt`, receipt).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  deleteReceipt(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/api/receipt/${id}`).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }
}
