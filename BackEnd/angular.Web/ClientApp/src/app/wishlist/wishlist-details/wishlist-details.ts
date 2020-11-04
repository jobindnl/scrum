import { IBook } from "../../book/book";
import { Iwishlist } from "../wishlist";

export interface IwishlistDetails {
  Id: number;
  WishlistId: number;
  SearchString: string;
  Quantity: number;
  BookId: number;
  Book: IBook;
}
