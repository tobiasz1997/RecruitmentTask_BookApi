import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Book } from '../book.model';
import { NgForm } from '@angular/forms';
import { BooksService } from '../books.service';

@Component({
  selector: 'app-book-add',
  templateUrl: './book-add.component.html',
  styleUrls: ['./book-add.component.css']
})
export class BookAddComponent {

  constructor(public booksService: BooksService) { }

  onAddBook(form: NgForm) {
    if (form.invalid) {return; }
    // tslint:disable-next-line: radix
    const pages = parseInt(form.value.pages);
    // tslint:disable-next-line: max-line-length
    this.booksService.addBook(form.value.title, form.value.author, form.value.category,
                                form.value.publishingCompany, form.value.description, pages);
    form.resetForm();
  }

}
