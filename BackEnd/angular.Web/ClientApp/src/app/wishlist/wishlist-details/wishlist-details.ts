import { IBook } from "../../book/book";
import { IWishlist } from "../wishlist";

export interface IWishlistDetail {
  id: number;
  quantity: number;
  wishlistid: number;
  wishlis: IWishlist;
  bookid: number;
  book: IBook;
}
