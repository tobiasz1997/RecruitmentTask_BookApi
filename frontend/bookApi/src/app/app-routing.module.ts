import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookListComponent } from './books/book-list/book-list.component';
import { BookAddComponent } from './books/book-add/book-add.component';
import { BookUpdateComponent } from './books/book-update/book-update.component';

const routes: Routes = [
  { path: '', component: BookListComponent},
  { path: 'create', component: BookAddComponent},
  { path: 'edit/:bookId', component: BookUpdateComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
