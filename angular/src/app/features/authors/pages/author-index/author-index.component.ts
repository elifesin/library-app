import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { AuthorService } from '../../services/author.service';
import { AuthorDto } from '../../models/author.dto';

@Component({
  selector: 'app-author-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './author-index.component.html',
  styleUrls: ['./author-index.component.css']
})

export class AuthorListComponent implements OnInit {
  authors = signal<AuthorDto[]>([]);

  showDeleteModal: boolean = false;
  authorIdToDelete: number | null = null;

  activeDropdownId: number | null = null;

  constructor(private authorService: AuthorService) {}

  ngOnInit(): void {
    this.loadAuthors();
  }

  toggleDropdown(id: number): void {
    if (this.activeDropdownId === id) {
      this.activeDropdownId = null; // Zaten açıksa kapat
    } else {
      this.activeDropdownId = id; // Değilse sadece bunu aç
    }
  }

  openDeleteModal(id: number): void {
    this.authorIdToDelete = id;
    this.showDeleteModal = true;
    this.activeDropdownId = null;
  }

  closeModal(): void {
    this.showDeleteModal = false;
    this.authorIdToDelete = null;
  }

  confirmDelete(): void {
    if (this.authorIdToDelete) {
      this.authorService.delete(this.authorIdToDelete).subscribe({
        next: () => {
          this.closeModal();
          this.loadAuthors();
        },
        error: (err) => {
          console.error('Yazar silinirken bir hata oluştu:', err);
          this.closeModal();
        }
      });
    }
  }

  loadAuthors(): void {
    this.authorService.getAll().subscribe({
      next: (data: AuthorDto[]) => {
        this.authors.set(data); // atama yerine .set() kullanıyoruz
      },
      error: (err) => {
        console.error('Yazarlar çekilirken bir hata oluştu:', err);
      }
    });
  }
}