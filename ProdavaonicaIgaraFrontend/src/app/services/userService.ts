import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = environment.apiUrl; // Zamijeni s URL-om tvog ASP.NET API-ja

  constructor(private http: HttpClient) { }

  getUsers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/User`).pipe(
      map(response => response as any[]) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }
}
