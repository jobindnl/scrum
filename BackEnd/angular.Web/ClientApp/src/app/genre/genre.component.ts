import { Component, OnInit} from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthorService } from '../author/author.service';
import { GenreService } from './genre.service';


@Component({
  selector: 'app-genre',
  templateUrl: './genre.component.html',
  styleUrls: ['./genre.component.css']
})

export class GenreComponent implements OnInit {
  public genreData: Array<any>;
  public bookData: Array<any>;
  public authorData: Array<any>; 

  constructor(public fb: FormBuilder, private genreService: GenreService, private authorService: AuthorService ) {
    genreService.getAll().subscribe((data: any) => this.genreData = data);
    authorService.getAll().subscribe((data: any) => this.authorData = data); 

    
  }
  genreSearchForm = this.fb.group({
    genre: ['']
  })

  onSubmit() {
    let genreId = this.genreSearchForm.get('genre').value;
    this.genreService.searchBooks(genreId).subscribe((data: any) => this.bookData = data);
    this.bookData.forEach(book => {
      let authorName = ""; 
      this.authorData.forEach(author => {
        if (author.id == book.authorId) { authorName=author.name }
      });

    });
  }

  ngOnInit() {
  }


}
