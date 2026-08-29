import { Component, OnInit, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { BookService } from '../../services/book.service';
import { BookDto } from '../../models/book.dto';

@Component({
  selector: 'app-book-index',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './book-index.component.html',
  styleUrls: ['./book-index.component.css']
})
export class BookIndexComponent implements OnInit {
  private bookService = inject(BookService);
  
  books = signal<BookDto[]>([]);

  // Modal ve Dropdown değişkenleri
  showDeleteModal: boolean = false;
  bookIdToDelete: number | null = null;
  activeDropdownId: number | null = null;

  ngOnInit(): void {
    this.loadBooks();
  }

  toggleDropdown(id: number): void {
    this.activeDropdownId = this.activeDropdownId === id ? null : id;
  }

  openDeleteModal(id: number): void {
    this.bookIdToDelete = id;
    this.showDeleteModal = true;
    this.activeDropdownId = null; // Modalı açınca menüyü kapat
  }

  closeModal(): void {
    this.showDeleteModal = false;
    this.bookIdToDelete = null;
  }

  confirmDelete(): void {
    if (this.bookIdToDelete) {
      this.bookService.Delete(this.bookIdToDelete).subscribe({
        next: () => {
          this.closeModal();
          this.loadBooks(); // Listeyi yenile
        },
        error: (err) => {
          console.error('Kitap silinirken hata oluştu:', err);
          this.closeModal();
        }
      });
    }
  }

  loadBooks(): void {
    this.bookService.getAll().subscribe({
      next: (data: BookDto[]) => {
        this.books.set(data);
      },
      error: (err) => {
        console.error('Kitaplar yüklenirken hata oluştu:', err);
      }
    });
  }
}