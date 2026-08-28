import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PublisherCreateDto, PublisherDto } from '../models/publisher.dto';
import { environment } from '../../../../environments/environment.development';

@Injectable({
    providedIn: 'root'
})
export class PublisherService {
    private api_url = `${environment.apiUrl}/api/publishers`;

    constructor(private http: HttpClient){}

    getAll(): Observable<PublisherDto[]>{
        return this.http.get<PublisherDto[]>(this.api_url);
    }

    getById(id: number): Observable<PublisherDto>{
        return this.http.get<PublisherDto>(`${this.api_url}/${id}`);
    }

    Insert(publisher: PublisherCreateDto): Observable<PublisherDto>{
        return this.http.post<PublisherDto>(this.api_url, publisher);
    }

    Update(id: number, publisher: PublisherDto): Observable<void>{
        return this.http.put<void>(`${this.api_url}/${id}`, publisher);
    }

    Delete(id: number): Observable<void>{
        return this.http.delete<void>(`${this.api_url}/${id}`);
    }
}
