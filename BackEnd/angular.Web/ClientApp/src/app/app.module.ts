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

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    UserProfileComponent,
    BookComponent,
    AuthorComponent,
    GenreComponent,
    BookFormComponent,
    GenreFormComponent,
    AuthorFormComponent,
    RegisterComponent,
    CreditCardComponent,
    CreditCardFormComponent
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
      { path: 'register-login', component: RegisterComponent },
      { path: 'credit-card', component: CreditCardComponent },
      { path: 'credit-card-add', component: CreditCardFormComponent },
      { path: 'credit-card-edit/:id', component: CreditCardFormComponent },
    ])
  ],
  providers: [BookService,
    AuthGuardService,
    AccountService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
