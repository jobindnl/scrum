import { IAddress } from "../address/address";
import { ICreditCard } from "../credit-card/credit-card";

export interface IUserProfile {
  id: number;
  name: string;
  nickName: string;
  email: string;
  password: string;
  homeAddressId: number;
  homeAddress: IAddress;
  defaultCreditCardId: number;
  defaultCreditCard: ICreditCard;
  shippingAddresses: IAddress[];
  creditCards: ICreditCard[];
}
