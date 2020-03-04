import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { BooksService } from '../books.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Book } from '../book.model';

@Component({
  selector: 'app-book-update',
  templateUrl: './book-update.component.html',
  styleUrls: ['./book-update.component.css']
})
export class BookUpdateComponent implements OnInit {

  private bookId: string;
  book: Book;

  constructor(public booksService: BooksService, public route: ActivatedRoute) { }

  ngOnInit() {
    this.route.paramMap.subscribe((paramMap: ParamMap) => {
      if (paramMap.has('bookId')) {
        this.bookId = paramMap.get('bookId');
        this.book = this.booksService.getBook(this.bookId);
        // this.postsService.getPost(this.postId).subscribe(postData => {
        //   this.loading = false;
        //   this.post = {id: postData._id, title: postData.title, content: postData.content };
        // });
      } else {
        console.log('isnotgood');
      }
    });
  }

  onUpdateBook(form: NgForm) {
    if (form.invalid) {return; }
    this.booksService.updateBook(this.bookId, form.value.description);
    form.resetForm();
  }

}
