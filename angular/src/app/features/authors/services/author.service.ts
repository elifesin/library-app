import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthorDto, AuthorCreateDto, AuthorEditDto } from '../models/author.dto'; 
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  
  private apiUrl = `http://localhost:5147/api/authors`; 

  constructor(private http: HttpClient) { }

  getAll(): Observable<AuthorDto[]> {
    return this.http.get<AuthorDto[]>(this.apiUrl);
  }
 
  getById(id: number): Observable<AuthorDto> {
    return this.http.get<AuthorDto>(`${this.apiUrl}/${id}`);
  }

  Insert(author: AuthorCreateDto): Observable<AuthorDto> {
    return this.http.post<AuthorDto>(this.apiUrl, author);
  }

  Update(id: number, author: AuthorEditDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, author);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}