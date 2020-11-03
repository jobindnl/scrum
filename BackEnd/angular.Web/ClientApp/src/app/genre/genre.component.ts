import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';


@Component({
  selector: 'app-genre',
  templateUrl: './genre.component.html',
  styleUrls: ['./genre.component.css']
})
export class GenreComponent implements OnInit {

  constructor(public fb: FormBuilder) { }
  genreSearchForm = this.fb.group({
    genre: ['']
  })


  ngOnInit() {
  }

}
