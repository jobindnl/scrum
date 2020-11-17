import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthorService } from './author.service';


@Component({
  selector: 'app-author',
  templateUrl: './author.component.html',
  styleUrls: ['./author.component.css']
})

export class AuthorComponent implements OnInit {
  public authorData: Array<any>;
  public bookData: Array<any>;

  constructor(public fb: FormBuilder, private authorService: AuthorService) {
  authorService.getAll().subscribe((data: any) => this.authorData = data);


  }
  authorSearchForm = this.fb.group({
    author: ['']
  })

  onSubmit() {
   let authorId = this.authorSearchForm.get('author').value;
   this.authorService.searchBooks(authorId).subscribe((data: any) => this.bookData = data);
  }

  ngOnInit() {
  }
}
