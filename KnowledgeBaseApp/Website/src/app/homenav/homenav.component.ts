import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, RouterLink, RouterOutlet } from '@angular/router';

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
          <a [routerLink]="['/create']" class="nav-button">Create</a>
        }
        <a [routerLink]="['/login']" class="nav-button">Log In</a>
      </div>
    </section>
  `,
  styleUrl: './homenav.component.css'
})
export class HomenavComponent {
  isLoggedIn: boolean = true;
}
