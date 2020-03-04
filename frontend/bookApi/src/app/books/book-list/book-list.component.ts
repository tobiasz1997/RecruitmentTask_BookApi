import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Book } from '../book.model';
import { BooksService } from '../books.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit, OnDestroy {

  books: Book[] = [];
  private booksSub: Subscription;

  constructor(public booksService: BooksService) { }

  ngOnInit() {
    this.booksService.getBooks();
    this.booksSub = this.booksService.getBookUpdated()
      .subscribe((books: Book[]) => {
        this.books = books;
      });
  }

  onDelete(bookId: string) {
    this.booksService.deleteBook(bookId);
  }

  ngOnDestroy() {
    this.booksSub.unsubscribe();
  }

}
