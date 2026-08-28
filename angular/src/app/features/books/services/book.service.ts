import { Injectable, Service } from '@angular/core';
import { BookCreateDto, BookDto, BookEditDto} from '../../books/models/book.dto'
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';


@Injectable({
    providedIn: 'root'
})
export class BookService {
    private api_url = `${environment.apiUrl}/api/books`;

    constructor(private http: HttpClient){}

    getAll(): Observable<BookDto[]>{
        return this.http.get<BookDto[]>(this.api_url);
    }

    getById(id: number): Observable<BookDto>{
        return this.http.get<BookDto>(`${this.api_url}/${id}`);
    }

    getAvailableBooks(): Observable<BookDto[]>{
        return this.http.get<BookDto[]>(`${this.api_url}/available`);
    }

    getBooksByCategory(categoryId: number): Observable<BookDto[]> {
        return this.http.get<BookDto[]>(`${this.api_url}/book-category/${categoryId}`);
  }

    Insert(book: BookCreateDto): Observable<BookDto>{
        return this.http.post<BookDto>(this.api_url, book);
    }

    Update(id: number, book: BookEditDto): Observable<BookDto>{
        return this.http.put<BookDto>(`${this.api_url}/${id}`, book);
    }

    Delete(id: number): Observable<void>{
        return this.http.delete<void>(`${this.api_url}/${id}`);
    }
}
