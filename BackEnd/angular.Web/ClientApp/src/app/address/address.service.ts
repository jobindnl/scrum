import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAddress } from './address';

@Injectable({
  providedIn: 'root'
})
export class AddressService {

  private apiURL = this.baseUrl + "api/address";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getAddresses(): Observable<IAddress[]> {
    return this.http.get<IAddress[]>(this.apiURL);
  }

  getAddress(addressId: string): Observable<IAddress> {
    return this.http.get<IAddress>(this.apiURL + '/' + addressId);
  }

  createAddress(address: IAddress): Observable<IAddress> {
    return this.http.post<IAddress>(this.apiURL, address);
  }

  updateAddress(address: IAddress): Observable<IAddress> {
    return this.http.put<IAddress>(this.apiURL + '/' + address.id.toString(), address);
  }

  deleteAddress(addressId: string): Observable<IAddress> {
    return this.http.delete<IAddress>(this.apiURL + '/' + addressId);
  }

}
