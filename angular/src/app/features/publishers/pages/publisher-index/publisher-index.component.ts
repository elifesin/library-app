import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { PublisherService } from '../../service/publisher.service';
import { PublisherDto } from '../../models/publisher.dto';

@Component({
  selector: 'app-publisher-index',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './publisher-index.component.html',
  styleUrl: './publisher-index.component.css',

})
export class PublisherIndexComponent implements OnInit{
  publishers = signal<PublisherDto[]>([]);

  activeDropdownId: number | null = null;
  publisherIdToDelete: number | null = null;
  showDeleteModal: boolean = false;

  constructor(private publisherService: PublisherService){}

  ngOnInit(): void{
    this.loadPublishers();
  }

  toggleDropdown(id: number): void{
    if(this.activeDropdownId === id){
      this.activeDropdownId === null;
    }
    else{
      this.activeDropdownId = id;
    }
  }

  openDeleteModal(id: number){
    this.publisherIdToDelete = id;
    this.showDeleteModal = true;
    this.activeDropdownId = null;
  }

  closeModal(): void{
    this.showDeleteModal = false;
    this.publisherIdToDelete = null;
  }

  confirmDelete(): void {
      if (this.publisherIdToDelete) {
        this.publisherService.Delete(this.publisherIdToDelete).subscribe({
          next: () => {
            this.closeModal();
            this.loadPublishers();
          },
          error: (err) => {
            console.error('Yayınevi silinirken bir hata oluştu:', err);
            this.closeModal();
          }
        });
      }
    }
  
    loadPublishers(): void {
      this.publisherService.getAll().subscribe({
        next: (data: PublisherDto[]) => {
          this.publishers.set(data);
        },
        error: (err) => {
          console.error('Yayınevleri çekilirken bir hata oluştu:', err);
        }
      });
    }
}
