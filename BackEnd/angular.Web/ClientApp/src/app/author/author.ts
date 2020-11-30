import { IBook } from "../book/book";

export interface IAuthor {
  id: number;
  name: number;
  biography: string;
  books: IBook[];
}
