import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';
import { UserNavComponent } from '../user-nav/user-nav.component';
import { UserService } from '../../services/user.service';
import { IUserProfile } from '../../models/IUserProfile';
import { UserDetailsService } from '../../services/user-details.service';
import { LocalStorageService } from '../../services/local-storage.service';

@Component({
  selector: 'app-user-home',
  standalone: true,
  imports: [CommonModule, RouterOutlet, UserNavComponent],
  template: `
  <section class="user-home">
  <app-user-nav></app-user-nav> 
  @defer () {
    <section class="user-home-content">
      <h2>Welcome back, <span>{{userDetails.firstName}}</span>!</h2>
  
    </section>

  } @loading () {
    <h3>loading content . . .</h3>
  }

    <div class="routes">
      <router-outlet></router-outlet>

    </div>

  </section>
  `,
  styleUrl: './user-home.component.css'
})
export class UserHomeComponent {
private userService = inject(UserService);
private userDetailsService = inject(UserDetailsService);
private localStorageService = inject(LocalStorageService)

userDetails: IUserProfile = {
  userProfileId: null,
  email: null,
  password: null,
  nametag: null,
  firstName: null,
  lastName: null,
  creationDate: null,
  birthDate: null
}
  constructor() {

    this.userService.getUserProfileAsync().then(result => {
      this.userDetails = result;
    })
  }


}
