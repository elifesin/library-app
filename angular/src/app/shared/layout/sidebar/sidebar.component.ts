import { Component } from '@angular/core';
import { RouterModule, RouterLinkActive } from '@angular/router';
 
@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  imports: [RouterModule, RouterLinkActive],
})
export class SidebarComponent {
  menuStates: { [key: string]: boolean } = {
    library: true,
    lab: true,
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
 