import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {
  private apiUrl = 'https://localhost:7174'; // Zamijeni s URL-om tvog ASP.NET API-ja

  constructor(private http: HttpClient) { }

  getArticle(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/articles/${id}`).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  getArticles(params: any): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/articles`, { params }).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  createArticle(article: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/articles`, article).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  updateArticle(article: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/articles/${article.id}`, article).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }

  deleteArticle(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/articles/${id}`).pipe(
      map(response => response as any) // Ako je potrebno mapiranje, ovdje možeš koristiti mapirati na svoje DTO modele
    );
  }
}
