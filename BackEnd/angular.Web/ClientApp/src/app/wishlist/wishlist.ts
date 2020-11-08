import { IWishlistDetail } from "./wishlist-details/wishlist-details";

export interface IWishlist {
  id: number;
  name: string;
  userId: number;
  wishlistDetails: IWishlistDetail[];
}
