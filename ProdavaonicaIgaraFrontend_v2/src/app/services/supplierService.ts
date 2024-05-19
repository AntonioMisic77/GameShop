import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SupplierService {
  private apiUrl = 'https://localhost:7174'; // Zamijeni s URL-om tvog ASP.NET API-ja

  constructor(private http: HttpClient) { }

  getSuppliers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/suppliers`).pipe(
      map(response => response as any[]) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  createSupplier(supplier: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/suppliers`, supplier).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  updateSupplier(supplier: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/suppliers/${supplier.id}`, supplier).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  deleteSupplier(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/suppliers/${id}`).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }
}
