import { Component, OnInit } from '@angular/core';
import { IAuthor } from './author';

@Component({
  selector: 'app-author',
  templateUrl: './author.component.html',
  styleUrls: ['./author.component.css']
})
export class AuthorComponent implements OnInit {
  author: IAuthor

  constructor() { }

  ngOnInit() {
  }

}
