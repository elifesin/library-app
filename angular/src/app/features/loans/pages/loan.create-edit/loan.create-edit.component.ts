import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoanService } from '../../../loans/services/loan.service';
import { BookService } from '../../../books/services/book.service';
import { MemberService } from '../../../members/services/member.service'; // Üye servisin olduğunu varsayıyoruz

@Component({
  selector: 'app-loan-create-edit',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './loan.create-edit.component.html'
})
export class LoanCreateEditComponent implements OnInit {
  private fb = inject(FormBuilder);
  private loanService = inject(LoanService);
  private bookService = inject(BookService);
  private memberService = inject(MemberService);
  private router = inject(Router);

  books: any[] = [];
  members: any[] = [];

  today = new Date().toISOString().split('T')[0];
  twoWeeksLater = new Date(new Date().setDate(new Date().getDate() + 14)).toISOString().split('T')[0];

  form = this.fb.group({
    memberID: [null as number | null, Validators.required],
    bookID: [null as number | null, Validators.required],
    loanDate: [this.today, Validators.required],
    dueDate: [this.twoWeeksLater, Validators.required]
  });

  ngOnInit(): void {
    this.bookService.getAvailableBooks().subscribe(data => this.books = data);
    this.memberService.getAll().subscribe(data => this.members = data);
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const formValue = this.form.getRawValue();

    this.loanService.Insert({
      memberID: Number(formValue.memberID),
      bookID: Number(formValue.bookID),
      loanDate: formValue.loanDate!,
      dueDate: formValue.dueDate!,
      returnDate: null
    }).subscribe({
      next: () => this.router.navigate(['/loans']),
      error: (err) => console.error('Ödünç verme hatası:', err)
    });
  }

  cancel(): void {
    this.router.navigate(['/loans']);
  }
}