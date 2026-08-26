import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { CategoryService } from '../../services/category.service';
import { CategoryDto, CategoryCreateDto } from '../../models/category.dto';

@Component({
  selector: 'app-author-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './category-index.component.html',
  styleUrls: ['./category-index.component.css']
})

export class CategoryListComponent implements OnInit {
  categories = signal<CategoryDto[]>([]);

  showDeleteModal: boolean = false;
  categoryIdToDelete: number | null = null;

  activeDropdownId: number | null = null;

  constructor(private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  toggleDropdown(id: number): void {
    if (this.activeDropdownId === id) {
      this.activeDropdownId = null; 
    } else {
      this.activeDropdownId = id;
    }
  }

  openDeleteModal(id: number): void {
    this.categoryIdToDelete = id;
    this.showDeleteModal = true;
    this.activeDropdownId = null;
  }

  closeModal(): void {
    this.showDeleteModal = false;
    this.categoryIdToDelete = null;
  }

  confirmDelete(): void {
    if (this.categoryIdToDelete) {
      this.categoryService.Delete(this.categoryIdToDelete).subscribe({
        next: () => {
          this.closeModal();
          this.loadCategories();
        },
        error: (err) => {
          console.error('Kategori silinirken bir hata oluştu:', err);
          this.closeModal();
        }
      });
    }
  }

  loadCategories(): void {
    this.categoryService.getAll().subscribe({
      next: (data: CategoryDto[]) => {
        this.categories.set(data);
      },
      error: (err) => {
        console.error('Kategoriler çekilirken bir hata oluştu:', err);
      }
    });
  }
}