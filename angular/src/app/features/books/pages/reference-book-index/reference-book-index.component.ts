import { Component, OnInit, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookService } from '../../services/book.service';
import { BookDto } from '../../models/book.dto';

@Component({
  selector: 'app-reference-book-index',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './reference-book-index.component.html'
})
export class ReferenceBookIndexComponent implements OnInit {
  private bookService = inject(BookService);
  
  books = signal<BookDto[]>([]);

  ngOnInit(): void {
    this.loadReferenceBooks();
  }

  loadReferenceBooks(): void {
    this.bookService.getBooksByCategory(3002).subscribe({
      next: (data: BookDto[]) => {
        this.books.set(data);
      },
      error: (err) => {
        console.error('Kaynak kitaplar yüklenirken hata oluştu:', err);
      }
    });
  }
}