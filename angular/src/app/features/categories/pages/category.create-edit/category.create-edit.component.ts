import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-category-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './category.create-edit.component.html',
})
export class CategoryEditCreateComponent implements OnInit {
  private fb = inject(FormBuilder);
  private categoryService = inject(CategoryService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  isEditMode = false;
  categoryId: number | null = null;

  form = this.fb.group({
    categoryName: ['', Validators.required]
  });

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');

    if (idParam) {
      this.isEditMode = true;
      this.categoryId = +idParam;
      this.loadCategory(this.categoryId);
    }
  }

  loadCategory(id: number): void {
    this.categoryService.getById(id).subscribe({
      next: (category) => {
        this.form.patchValue({
          categoryName: category.categoryName,
        });
      },
      error: (err) => console.error('Kategori getirilirken hata oluştu:', err)
    });
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const formValue = this.form.getRawValue();

    if (this.isEditMode && this.categoryId) {
      this.categoryService.Update(this.categoryId, {
        id: this.categoryId,
        categoryName: formValue.categoryName!,
      }).subscribe({
        next: () => this.router.navigate(['/categories']),
        error: (err) => console.error('Güncelleme hatası:', err)
      });
    } else {
      this.categoryService.Insert({
        categoryName: formValue.categoryName!,
      }).subscribe({
        next: () => this.router.navigate(['/categories']),
        error: (err) => console.error('Oluşturma hatası:', err)
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/categories']);
  }
}