import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { MemberService } from '../../services/member.service';
import { MemberDto } from '../../models/member.dto';

@Component({
  selector: 'app-member-index.component',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './member-index.component.html',
  styleUrl: './member-index.component.css',
})
export class MemberIndexComponent implements OnInit {
    members = signal<MemberDto[]>([]);
    showDeleteModal: boolean = false;
    memberIdToDelete: number | null = null;
    activeDropdownId: number | null = null;

    constructor(private memberService: MemberService){}

    ngOnInit(): void {
    this.loadMembers();
  }

  toggleDropdown(id: number): void {
    if (this.activeDropdownId === id) {
      this.activeDropdownId = null; 
    } else {
      this.activeDropdownId = id;
    }
  }

   openDeleteModal(id: number): void {
    this.memberIdToDelete = id;
    this.showDeleteModal = true;
    this.activeDropdownId = null;
  }

  closeModal(): void {
    this.showDeleteModal = false;
    this.memberIdToDelete = null;
  }

  confirmDelete(): void {
    if (this.memberIdToDelete) {
      this.memberService.Delete(this.memberIdToDelete).subscribe({
        next: () => {
          this.closeModal();
          this.loadMembers();
        },
        error: (err) => {
          console.error('Yazar silinirken bir hata oluştu:', err);
          this.closeModal();
        }
      });
    }
  }

  loadMembers(): void {
      this.memberService.getAll().subscribe({
        next: (data: MemberDto[]) => {
          this.members.set(data);
        },
        error: (err) => {
          console.error('Yazarlar çekilirken bir hata oluştu:', err);
        }
      });
    }
}
