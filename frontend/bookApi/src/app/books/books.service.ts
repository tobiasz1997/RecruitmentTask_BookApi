import { Book } from './book.model';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({providedIn: 'root'})
export class BooksService {
  private books: Book[] = [];
  private booksUpdated = new Subject<Book[]>();

  constructor(public http: HttpClient, public router: Router) { }

  getBooks() {
    this.http.get<Book[]>('http://localhost:5000/book')
      .subscribe((booksData) => {
        this.books = booksData;
        this.booksUpdated.next([...this.books]);
      });
  }

  getBookUpdated() {
    return this.booksUpdated.asObservable();
  }

  addBook(title: string, author: string, category: string, publishingCompany: string, description: string, pages: number) {
    const book: Book = {
      id: '42584450-59a0-4328-85e8-ce769d17d233',
      title,
      author,
      category,
      publishingCompany,
      description,
      pages
    };
    console.log(book);
    this.http.post<{bookId: string}>('http://localhost:5000/book', book)
    .subscribe(responseData => {
      console.log('Added successfully !');
      const fetchedId = responseData.bookId;
      book.id = fetchedId;
      this.books.push(book);
      this.booksUpdated.next([...this.books]);
    });
  }

  updateBook(id: string, description: string) {
    const bookData = { id, description};
    this.http.put('http://localhost:5000/book/' + id, bookData)
     .subscribe(() => {
       const updateBook = [...this.books];
       const oldBookIndex = updateBook.findIndex(x => x.id === id);
       updateBook[oldBookIndex].description = bookData.description;
       this.books = updateBook;
       console.log(this.books);
       this.booksUpdated.next([...this.books]);
       this.router.navigate(['/']);
    });
  }

  deleteBook(bookId: string) {
    this.http.delete('http://localhost:5000/book/' + bookId)
      .subscribe(() => {
        console.log('Deleted successfully !');
        const booksUpdated = this.books.filter(book => book.id !== bookId);
        this.books = booksUpdated;
        this.booksUpdated.next([...this.books]);
      });
  }

  getBook(id: string) {
    return {...this.books.find(x => x.id === id)};
  }
}
