import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoanCreateDto, LoanDto } from '../models/loan.dto';

@Injectable({
    providedIn: 'root'
})
export class LoanService {
    private api_url = 'http://localhost:5147/api/loans';

    constructor(private http: HttpClient){}

    getAll(): Observable<LoanDto[]>{
        return this.http.get<LoanDto[]>(this.api_url);
    }

    getById(id: number): Observable<LoanDto>{
        return this.http.get<LoanDto>(`${this.api_url}/${id}`);
    }

    Insert(loan: LoanCreateDto): Observable<LoanDto>{
        return this.http.post<LoanDto>(this.api_url, loan);
    }

    getLoansByMemberId(memberId: number): Observable<LoanDto>{
        return this.http.get<LoanDto>(`${this.api_url}/${memberId}`);
    }

    returnBook(loanId: number): Observable<LoanDto>{
        return this.http.post<LoanDto>(`${this.api_url}/${loanId}`);
    }
}
