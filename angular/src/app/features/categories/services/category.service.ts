import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CategoryDto, CategoryCreateDto } from '../models/category.dto';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

    private api_url = `http://localhost:5147/api/categories`;

    constructor(private http: HttpClient){}

    getAll(): Observable<CategoryDto[]>{
        return this.http.get<CategoryDto[]>(this.api_url);
    }

    getById(id: number): Observable<CategoryDto>{
        return this.http.get<CategoryDto>(`${this.api_url}/${id}`);
    }

    Insert(category: CategoryCreateDto): Observable<CategoryDto>{
        return this.http.post<CategoryDto>(this.api_url, category);
    }

    Update(id: number, category: CategoryDto): Observable<void>{
        return this.http.put<void>(`${this.api_url}/${id}`, category);
    }

    Delete(id: number): Observable<void>{
        return this.http.delete<void>(`${this.api_url}/${id}`);
    }
}
