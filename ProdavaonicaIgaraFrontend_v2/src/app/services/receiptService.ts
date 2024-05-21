import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environment';

@Injectable({
  providedIn: 'root'
})
export class ReceiptService {
  private apiUrl = environment.apiUrl; // Zamijeni s URL-om tvog ASP.NET API-ja

  constructor(private http: HttpClient) { }

  getReceipt(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/receipts/${id}`).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  getReceipts(params: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/receipt/paged`,  params ).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  createReceipt(receipt: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/receipt`, receipt).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  updateReceipt(receipt: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/receipt`, receipt).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  deleteReceipt(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/receipt/${id}`).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }
}
