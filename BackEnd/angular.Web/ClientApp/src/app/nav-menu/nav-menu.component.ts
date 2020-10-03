import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../account/account.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  constructor(private accountService: AccountService,
    private router: Router) {}


  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }


  logout() {
    this.accountService.logout();
    this.router.navigate(['/']);
  }

  loggedIn() {
    return this.accountService.loggedIn();
  }
}
