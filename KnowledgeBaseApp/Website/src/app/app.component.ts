import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink],
  template: `
    <main>
      <a [routerLink]="['/']">
        <header class="logo">Knowledge Base</header>
    </a>
    <section class="content">
      <router-outlet></router-outlet>
    </section>
    </main>
  `,
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'KnowledgeBase';
}
