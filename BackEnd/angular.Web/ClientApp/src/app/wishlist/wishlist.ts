import { IwishlistDetails } from "./wishlist-details/wishlist-details";

export interface Iwishlist {
  id: number;
  name: string;
  userId: number;
  wishlistDetails: IwishlistDetails[];
}
