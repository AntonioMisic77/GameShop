import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environment';

@Injectable({
  providedIn: 'root'
})
export class SupplierService {
  private apiUrl = environment.apiUrl; // Zamijeni s URL-om tvog ASP.NET API-ja

  constructor(private http: HttpClient) { }

  getSuppliers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/Supplier`).pipe(
      map(response => response as any[]) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  createSupplier(supplier: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/Supplier`, supplier).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  updateSupplier(supplier: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/Supplier/${supplier.id}`, supplier).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  deleteSupplier(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/Supplier/${id}`).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }
}
