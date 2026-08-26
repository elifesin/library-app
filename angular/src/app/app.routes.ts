import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { AuthorListComponent } from './features/authors/pages/author-index/author-index.component'; 
import {AuthorEditCreateComponent} from './features/authors/pages/author-create-edit-form/author-create-edit.component';
import {MemberIndexComponent} from './features/members/pages/member-index/member-index.component'
import { MemberCreateEditComponent } from './features/members/pages/member.create-edit/member.create-edit.component';
import { CategoryListComponent } from './features/categories/pages/category-index/category-index.component';
import { CategoryEditCreateComponent } from './features/categories/pages/category.create-edit/category.create-edit.component';
import { PublisherIndexComponent } from './features/publishers/pages/publisher-index/publisher-index.component';
import { PublisherEditCreateComponent } from './features/publishers/pages/publisher.create-edit/publisher.create-edit.component';

export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },

  { path: 'authors', component: AuthorListComponent },
  { path: 'authors/create', component: AuthorEditCreateComponent },
  { path: 'authors/edit/:id', component: AuthorEditCreateComponent },

  { path: 'members', component: MemberIndexComponent},
  { path: 'members/create', component: MemberCreateEditComponent},
  { path: 'members/edit/:id', component: MemberCreateEditComponent},

  { path: 'categories', component: CategoryListComponent},
  { path: 'categories/create', component: CategoryEditCreateComponent},
  { path: 'categories/edit/:id', component: CategoryEditCreateComponent},
  
  { path: 'publishers', component: PublisherIndexComponent},
  { path: 'publishers/create', component: PublisherEditCreateComponent},
  { path: 'publishers/edit/:id', component: PublisherEditCreateComponent},

];