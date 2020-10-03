import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IBook } from '../book';
import { BookService } from '../book.service';
  
@Component({
  selector: 'app-book-form',
  templateUrl: './book-form.component.html',
  styleUrls: ['./book-form.component.css']
})
export class BookFormComponent implements OnInit {

  constructor(private fb: FormBuilder,
    private bookService: BookService,
    private router: Router,
    private activatedRoute: ActivatedRoute ) { }


  editMode: boolean = false;
  formGroup: FormGroup;
  bookId: number;


  ngOnInit() {
    this.formGroup = this.fb.group({
      title: '',
      salesPrice: 0,
      description: '',
      publishingInfo:'',
      authorId: 0,
      genreId: 0
    });

    this.activatedRoute.params.subscribe(params => {
      if (params["id"] == undefined) {
        return;
      }
      this.editMode = true;

      this.bookId = params["id"];

      this.bookService.getBook(this.bookId.toString())
        .subscribe(book => this.loadForm(book),
          error => console.error(error));
    });
  }

  loadForm(book: IBook) {
    this.formGroup.patchValue({
      title: book.title,
      salesPrice: book.salesPrice,
      description: book.description,
      publishingInfo: book.publishingInfo,
      authorId: book.authorId,
      genreId: book.genreId
    })
  }

  save() {
    let book: IBook = Object.assign({}, this.formGroup.value);
    console.table(book);

    if (this.editMode) {
      //edit book
      
      var x: number = +this.bookId;
      book.id = x;
      this.bookService.updateBook(book)
        .subscribe(book => this.onSaveSuccess(),
          error => console.error(error));
    } else {
      //add book

      this.bookService.createBook(book)
        .subscribe(book => this.onSaveSuccess(),
          error => console.error(error));
    }
  }

  onSaveSuccess() {
    this.router.navigate(["/book"]);
  }


}
