import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MemberDto, MemberCreateDto } from '../models/member.dto';
import { Observable } from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class MemberService {
    private api_url = `http://localhost:5147/api/members`;

    constructor(private http: HttpClient){}

    getAll(): Observable<MemberDto[]>{
        return this.http.get<MemberDto[]>(this.api_url);
    }

    getById(id: number): Observable<MemberDto>{
        return this.http.get<MemberDto>(`${this.api_url}/${id}`);
    }
 
    Insert(member: MemberCreateDto): Observable<MemberDto>{
        return this.http.post<MemberDto>(this.api_url, member);
    }

    Update(id: number, member: MemberDto): Observable<void>{
        return this.http.put<void>(`${this.api_url}/${id}`, member);
    }

    Delete(id: number): Observable<void>{
        return this.http.delete<void>(`${this.api_url}/${id}`);
    }
}
