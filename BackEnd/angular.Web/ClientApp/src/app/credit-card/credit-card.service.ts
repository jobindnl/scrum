import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from '../account/account.service';
import { ICreditCard } from './credit-card';

@Injectable({
  providedIn: 'root'
})
export class CreditCardService {

  private apiURL = this.baseUrl + "api/CreditCard";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private accountService: AccountService) { }

  getCreditCards(): Observable<ICreditCard[]> {
    return this.http.get<ICreditCard[]>(this.apiURL);
  }

  getCreditCard(creditcardId: string): Observable<ICreditCard> {
    return this.http.get<ICreditCard>(this.apiURL + '/' + creditcardId);
  }

  createCreditCard(creditcard: ICreditCard): Observable<ICreditCard> {
    return this.http.post<ICreditCard>(this.apiURL, creditcard);
  }

  updateCreditCard(creditcard: ICreditCard): Observable<ICreditCard> {
    return this.http.put<ICreditCard>(this.apiURL + '/' + creditcard.id.toString(), creditcard);
  }

  deleteCreditCard(creditcardId: string): Observable<ICreditCard> {
    return this.http.delete<ICreditCard>(this.apiURL + '/' + creditcardId);
  }

}
