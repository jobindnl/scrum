import { IBook } from '../../book/book';
export interface IwishlistDetails {
  Id: number;
  WishlistId: number;
  SearchString: string;
  Quantity: number;
  BookId: number;
  Books: IBook[];
}
