import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { BookComponent } from './book/book.component';
import { AuthorComponent } from './author/author.component';
import { GenreComponent } from './genre/genre.component';
import { GenreService } from './genre/genre.service'; 
import { BookFormComponent } from './book/book-form/book-form.component';
import { GenreFormComponent } from './genre/genre-form/genre-form.component';
import { AuthorFormComponent } from './author/author-form/author-form.component';
import { BookService } from './book/book.service';
import { RegisterComponent } from './account/register/register.component';
import { AuthGuardService } from './services/auth-guard.service';
import { AccountService } from './account/account.service';
import { AuthInterceptorService } from './services/auth-interceptor.service';
import { CreditCardComponent } from './credit-card/credit-card.component';
import { CreditCardFormComponent } from './credit-card/credit-card-form/credit-card-form.component'
import { AddressComponent } from './address/address.component';
import { AddressFormComponent } from './address/address-form/address-form.component';
import { UserProfileFormComponent } from './user-profile/user-profile-form/user-profile-form.component';
import { ForgotPasswordComponent } from './account/forgot-password/forgot-password.component';
import { EmailConfirmSentComponent } from './account/email-confirm-sent/email-confirm-sent.component';
import { BookDetailComponent } from './book/book-detail/book-detail.component';
import { PwdChangeComponent } from './account/pwd-change/pwd-change.component';
import { VerifyTokenResetPasswordComponent } from './account/verify-token-reset-password/verify-token-reset-password.component';
import { WishlistDetailsComponent } from './wishlist/wishlist-details/wishlist-details.component';
import { LogInterceptorService } from './services/log-interceptor.service';
import { WishlistComponent } from './wishlist/wishlist.component';
import { WishlistService } from './wishlist/wishlist.service';
import { WishlistFormComponent } from './wishlist/wishlist-form/wishlist-form.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';




@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    UserProfileComponent,
    UserProfileFormComponent,
    BookComponent,
    AuthorComponent,
    GenreComponent,
    BookFormComponent,
    GenreFormComponent,
    AuthorFormComponent,
    RegisterComponent,
    CreditCardComponent,
    CreditCardFormComponent,
    AddressComponent,
    AddressFormComponent,
    PwdChangeComponent,
    ForgotPasswordComponent,
    BookDetailComponent,
    EmailConfirmSentComponent,
    VerifyTokenResetPasswordComponent,
    WishlistComponent,
    WishlistFormComponent,
    WishlistDetailsComponent,
    ShoppingCartComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'genre', component: GenreComponent },
      { path: 'author', component: AuthorComponent },
      { path: 'book', component: BookComponent },
      { path: 'book-add', component: BookFormComponent, canActivate: [AuthGuardService] },
      { path: 'book-edit/:id', component: BookFormComponent, canActivate: [AuthGuardService] },
      { path: 'genre-add', component: GenreFormComponent, canActivate: [AuthGuardService] },
      { path: 'author-add', component: AuthorFormComponent, canActivate: [AuthGuardService] },
      { path: 'user-profile', component: UserProfileComponent, canActivate: [AuthGuardService] },
      { path: 'user-profile-edit', component: UserProfileFormComponent, canActivate: [AuthGuardService] },
      { path: 'register-login', component: RegisterComponent },
      { path: 'credit-card', component: CreditCardComponent },
      { path: 'credit-card-add', component: CreditCardFormComponent },
      { path: 'credit-card-edit/:id', component: CreditCardFormComponent },
      { path: 'address', component: AddressComponent },
      { path: 'address-add', component: AddressFormComponent },
      { path: 'address-edit/:id', component: AddressFormComponent },
      { path: 'pwd-change', component: PwdChangeComponent },
      { path: 'forgot-password', component: ForgotPasswordComponent },
      { path: 'book-detail/:id', component: BookDetailComponent },
      { path: 'email-confirm-sent', component: EmailConfirmSentComponent },
      { path: 'verify-token-reset-password', component: VerifyTokenResetPasswordComponent },
      { path: 'wishlist', component: WishlistComponent },
      { path: 'wishlist-add', component: WishlistFormComponent },
      { path: 'wishlist-edit/:id', component: WishlistFormComponent },
      { path: 'wishlist-details/:id', component: WishlistDetailsComponent },
      { path: 'shopping-cart', component: ShoppingCartComponent}
    ])
  ],
  providers: [BookService,
    AuthGuardService,
    AccountService,
    WishlistService,
    GenreService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true
    },
     {
      provide: HTTP_INTERCEPTORS,
      useClass: LogInterceptorService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
