import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user-nav',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
  <nav>
    <a routerLink="/">
      <header>Knowledge Base</header>
    </a>
    <ul class="nav-links">
      <li><a routerLink="notes">Notes</a></li>
      <li><a routerLink="excerpts">Excerpts</a></li>
      <li><a routerLink="">Profile</a></li>
      <li><a (click)="logout()">Log Out</a></li>
    </ul>
  </nav>
  `,
  styleUrl: './user-nav.component.css'
})
export class UserNavComponent {
  private userService = inject(UserService);
  private router = inject(Router);

  logout() {
    this.userService.logout();
    this.router.navigate(['/']);
  }
}
