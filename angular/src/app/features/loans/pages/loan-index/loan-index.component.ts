import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LoanService } from '../../services/loan.service';
import { LoanDto } from '../../models/loan.dto';

@Component({
  selector: 'app-loan-index',
  standalone: true,
  imports: [CommonModule, RouterModule],
  providers: [DatePipe],
  templateUrl: './loan-index.component.html'
})
export class LoanIndexComponent implements OnInit {
  private loanService = inject(LoanService);
  
  // Loans dizisini Signal olarak tanımlıyoruz
  loans = signal<LoanDto[]>([]);

  ngOnInit(): void {
    this.loadLoans();
  }

  loadLoans(): void {
    this.loanService.getAll().subscribe({
      next: (data) => this.loans.set(data), // Signal'e veriyi .set() ile yüklüyoruz
      error: (err) => console.error('Ödünç kayıtları yüklenirken hata oluştu:', err)
    });
  }

  returnBook(loanId: number): void {
    if (confirm('Bu kitabı iade edilmiş olarak işaretlemek istiyor musunuz?')) {
      this.loanService.returnBook(loanId).subscribe({
        next: () => {
          alert('Kitap başarıyla iade edildi!');
          this.loadLoans();
        },
        error: (err) => console.error('İade işlemi başarısız:', err)
      });
    }
  }
}