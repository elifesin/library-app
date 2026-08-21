import { Component } from '@angular/core';
import { RouterModule, RouterLinkActive } from '@angular/router'; 

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  imports: [RouterModule, RouterLinkActive], 
})
export class SidebarComponent {
  menuStates: { [key: string]: boolean } = {
    authors: false,
    books: false,
    categories: false,
    publishers: false,
    loans: false,
    members: false
  };

  // Tıklanan menünün adını alıp sadece onun durumunu tersine çeviren dinamik metot
  toggleMenu(menuName: string): void {
    this.menuStates[menuName] = !this.menuStates[menuName];
  }
}
