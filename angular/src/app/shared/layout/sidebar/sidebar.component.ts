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
    members: false,
  };

  toggleMenu(menuName: string): void {
    this.menuStates[menuName] = !this.menuStates[menuName];
  }
}
