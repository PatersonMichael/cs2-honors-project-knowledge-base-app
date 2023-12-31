import { Component, Input, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, RouterLink, RouterOutlet } from '@angular/router';
import { AppCookieService } from '../../services/app-cookie.service';
import { UserService } from '../../services/user.service';


@Component({
  selector: 'app-homenav',
  standalone: true,
  imports: [CommonModule, RouterModule, RouterLink, RouterOutlet],
  template: `
    <section class="home-nav-bar">
      <a [routerLink]="['/']">
        <header class="logo">Knowledge Base</header>
      </a>
      <div class="route-buttons">
        <a [routerLink]="['/about']" class="nav-button">About</a>
        @if (isLoggedIn) {
          <a [routerLink]="['/user']" class="nav-button">Create</a>
          <a (click)="logout()" class="nav-button">Log Out</a>
        }
        @else if (isOnLoginPage && !isLoggedIn) {
          <a [routerLink]="['/signup']" class="nav-button">Sign Up</a>
        }
        @else {
          <a [routerLink]="['/login']" class="nav-button">Log In</a>

        }
      </div>
    </section>
  `,
  styleUrl: './homenav.component.css'
})
export class HomenavComponent {
  isLoggedIn: boolean = false;
  appCookieService = inject(AppCookieService);
  userService = inject(UserService);

  constructor() {
    if (this.appCookieService.get("Authorization")) {
      this.isLoggedIn = true;
    }
  }


  @Input() isOnLoginPage = false;

  logout() {
    this.userService.logout();
    this.isLoggedIn = false;
  }
}
