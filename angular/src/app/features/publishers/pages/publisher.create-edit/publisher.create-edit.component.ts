import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PublisherService } from '../../service/publisher.service';

@Component({
  selector: 'app-publisher-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './publisher.create-edit.component.html',
})
export class PublisherEditCreateComponent implements OnInit {
  private fb = inject(FormBuilder);
  private publisherService = inject(PublisherService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  isEditMode = false;
  publisherId: number | null = null;

  form = this.fb.group({
  name: ['', Validators.required]
  });

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');

    if (idParam) {
      this.isEditMode = true;
      this.publisherId = +idParam;
      this.loadPublisher(this.publisherId);
    }
  }

  loadPublisher(id: number): void {
    this.publisherService.getById(id).subscribe({
      next: (publisher) => {
        this.form.patchValue({
          name: publisher.name,
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

    if (this.isEditMode && this.publisherId) {
      this.publisherService.Update(this.publisherId, {
        id: this.publisherId,
        name: formValue.name!,
      }).subscribe({
        next: () => this.router.navigate(['/publishers']),
        error: (err) => console.error('Güncelleme hatası:', err)
      });
    } else {
      this.publisherService.Insert({
        name: formValue.name!,
      }).subscribe({
        next: () => this.router.navigate(['/publishers']),
        error: (err) => console.error('Oluşturma hatası:', err)
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/publishers']);
  }
}