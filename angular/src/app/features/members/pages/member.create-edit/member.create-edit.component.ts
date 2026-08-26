import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MemberService } from '../../services/member.service';

@Component({
  selector: 'app-member.create-edit',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './member.create-edit.component.html',
})
export class MemberCreateEditComponent implements OnInit{
    private fb = inject(FormBuilder);
    private memberService = inject(MemberService);
    private route = inject(ActivatedRoute);
    private router = inject(Router);
  
    isEditMode = false;
    memberId: number | null = null;
  
    form = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required]
    });

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');

    if (idParam) {
      this.isEditMode = true;
      this.memberId = +idParam;
      this.loadMember(this.memberId);
    }
  }

  loadMember(id: number): void {
    this.memberService.getById(id).subscribe({
      next: (member) => {
        this.form.patchValue({
          firstName: member.firstName,
          lastName: member.lastName
        });
      },
      error: (err) => console.error('Üye getirilirken hata oluştu:', err)
    });
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const formValue = this.form.getRawValue();

    if (this.isEditMode && this.memberId) {
      this.memberService.Update(this.memberId, {
        id: this.memberId,
        firstName: formValue.firstName!,
        lastName: formValue.lastName!,
      }).subscribe({
        next: () => this.router.navigate(['/members']),
        error: (err) => console.error('Güncelleme hatası:', err)
      });
    } else {
      this.memberService.Insert({
        firstName: formValue.firstName!,
        lastName: formValue.lastName!
      }).subscribe({
        next: () => this.router.navigate(['/members']),
        error: (err) => console.error('Oluşturma hatası:', err)
      });
    }
  }

  cancel(): void {
    this.router.navigate(['/members']);
  }
}
