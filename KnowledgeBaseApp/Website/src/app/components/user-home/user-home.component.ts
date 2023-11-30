import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';
import { UserNavComponent } from '../user-nav/user-nav.component';
@Component({
  selector: 'app-user-home',
  standalone: true,
  imports: [CommonModule, RouterOutlet, UserNavComponent],
  template: `
  <section class="user-home">
  <app-user-nav></app-user-nav> 

    <div class="routes">
      <router-outlet></router-outlet>

    </div>

  </section>
  `,
  styleUrl: './user-home.component.css'
})
export class UserHomeComponent {

}
