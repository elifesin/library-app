import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { AuthorListComponent } from './features/authors/pages/author-index/author-index.component'; 
import {AuthorEditCreateComponent} from './features/authors/pages/author-create-edit-form/author-create-edit.component';

export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },

  { path: 'authors', component: AuthorListComponent },
     { path: 'authors/create', component: AuthorEditCreateComponent },
     { path: 'authors/edit/:id', component: AuthorEditCreateComponent },
];