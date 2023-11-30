import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

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
      <li><a routerLink="">Log Out</a></li>
    </ul>
  </nav>
  `,
  styleUrl: './user-nav.component.css'
})
export class UserNavComponent {

}
