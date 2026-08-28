import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { BookService } from '../../../books/services/book.service';
import { AuthorService } from '../../../authors/services/author.service';
import { CategoryService } from '../../../categories/services/category.service';
import { PublisherService } from '../../../publishers/service/publisher.service'; 

@Component({
  selector: 'app-book-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './book.create-edit.component.html',
})
export class BookCreateEditComponent implements OnInit {
  private fb = inject(FormBuilder);
  private bookService = inject(BookService);
  private authorService = inject(AuthorService);
  private categoryService = inject(CategoryService);
  private publisherService = inject(PublisherService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  isEditMode = false;
  bookId: number | null = null;

  authors: any[] = [];
  categories: any[] = [];
  publishers: any[] = [];

  form = this.fb.group({
    title: ['', Validators.required],
    publishYear: [new Date().getFullYear(), [Validators.required, Validators.min(1000)]],
    authorID: [null as number | null, Validators.required],
    categoryID: [null as number | null, Validators.required],
    publisherId: [null as number | null, Validators.required],
    isBorrowed: [false], 
    authorFullName: [''] 
  });

  ngOnInit(): void {
    this.loadDropdownData();

    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.isEditMode = true;
      this.bookId = +idParam;
      this.loadBook(this.bookId);
    }
  }

  loadDropdownData(): void {
    this.authorService.getAll().subscribe(data => this.authors = data);
    this.categoryService.getAll().subscribe(data => this.categories = data);
    this.publisherService.getAll().subscribe(data => this.publishers = data);
  }

  loadBook(id: number): void {
    this.bookService.getById(id).subscribe({
      next: (book) => {
        this.form.patchValue({
          title: book.title,
          publishYear: book.publishYear,
          isBorrowed: book.isBorrowed
        });
      },
      error: (err) => console.error('Kitap getirilirken hata oluştu:', err)
    });
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const formValue = this.form.getRawValue();

    if (this.isEditMode && this.bookId) {
      this.bookService.Update(this.bookId, {
        id: this.bookId,
        title: formValue.title!,
        publishYear: formValue.publishYear!,
        authorID: Number(formValue.authorID),
        categoryID: Number(formValue.categoryID),
        publisherId: Number(formValue.publisherId),
        isBorrowed: formValue.isBorrowed!
      }).subscribe({
        next: () => this.router.navigate(['/books']),
        error: (err) => console.error('Güncelleme hatası:', err)
      });
    } else {
      this.bookService.Insert({
        title: formValue.title!,
        publishYear: formValue.publishYear!,
        authorID: Number(formValue.authorID),
        categoryID: Number(formValue.categoryID),
        publisherId: Number(formValue.publisherId),
        authorFullName: 'Seçim Yapıldı' 
      }).subscribe({
        next: () => this.router.navigate(['/books']),
        error: (err) => console.error('Oluşturma hatası:', err)
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/books']);
  }
}