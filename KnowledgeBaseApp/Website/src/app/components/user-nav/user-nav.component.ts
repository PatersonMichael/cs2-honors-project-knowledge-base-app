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
      <li><a (click)="navTo('user/create')"><span id="accent">Create</span></a></li>
      <li><a (click)="navTo('user/notes')">Notes</a></li>
      <li><a (click)="navTo('user/excerpts')">Excerpts</a></li>
      <li><a (click)="navTo('user/profile')">Profile</a></li>
      <li><a (click)="logout()">Log Out</a></li>
    </ul>
  </nav>
  `,
  styleUrl: './user-nav.component.css'
})
export class UserNavComponent {
  private userService = inject(UserService);
  private router = inject(Router);

  selectedRoute: boolean = false;

  navTo(route: string) {
    this.router.navigate([route])
  }

  

  logout() {
    this.userService.logout();
    this.router.navigate(['/']);
  }
}
