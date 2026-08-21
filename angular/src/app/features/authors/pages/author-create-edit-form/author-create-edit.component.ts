import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorService } from '../../services/author.service';

@Component({
  selector: 'app-author-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './author-create-edit.component.html',
})
export class AuthorEditCreateComponent implements OnInit {
  private fb = inject(FormBuilder);
  private authorService = inject(AuthorService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  isEditMode = false;
  authorId: number | null = null;

  form = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required]
  });

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');

    if (idParam) {
      this.isEditMode = true;
      this.authorId = +idParam;
      this.loadAuthor(this.authorId);
    }
  }

  loadAuthor(id: number): void {
    this.authorService.getById(id).subscribe({
      next: (author) => {
        this.form.patchValue({
          firstName: author.firstName,
          lastName: author.lastName
        });
      },
      error: (err) => console.error('Yazar getirilirken hata oluştu:', err)
    });
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const formValue = this.form.getRawValue();

    if (this.isEditMode && this.authorId) {
      this.authorService.Update(this.authorId, {
        id: this.authorId,
        firstName: formValue.firstName!,
        lastName: formValue.lastName!,
      }).subscribe({
        next: () => this.router.navigate(['/authors']),
        error: (err) => console.error('Güncelleme hatası:', err)
      });
    } else {
      this.authorService.Insert({
        firstName: formValue.firstName!,
        lastName: formValue.lastName!
      }).subscribe({
        next: () => this.router.navigate(['/authors']),
        error: (err) => console.error('Oluşturma hatası:', err)
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/authors']);
  }
}