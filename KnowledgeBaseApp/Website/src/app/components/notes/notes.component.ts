import { Component, Input, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserNavComponent } from '../user-nav/user-nav.component';
import { IUserProfile } from '../../models/IUserProfile';
import { UserDetailsService } from '../../services/user-details.service';
import { LocalStorageService } from '../../services/local-storage.service';

@Component({
  selector: 'app-notes',
  standalone: true,
  imports: [CommonModule, UserNavComponent],
  template: `
      <div class="notes-component">
        <section class="notes">
          <div class="note-card">
            <article class="note-card-body">
              <div class="note-card-heading">
                <h2 class="note-card-title">My Note</h2>
                <img src="assets/edit-icon-black.png" alt="Edit" class="edit-icon">
              </div>
              <p class="note-text">Lorem Ipsum...</p>
            </article>
          </div>
          <div class="note-card">
            <article class="note-card-body">
              <div class="note-card-heading">
                <h2 class="note-card-title">My Note</h2>
                <img src="assets/edit-icon-black.png" alt="Edit" class="edit-icon">
              </div>
              <p class="note-text">Lorem Ipsum...</p>
            </article>
          </div>
        </section>
    
      </div>
  `,
  styleUrl: './notes.component.css'
})
export class NotesComponent {
  
  // isLoaded: boolean = false;
  userDetailsService = inject(UserDetailsService);
  localStorageService = inject(LocalStorageService);
  userDetails: IUserProfile = JSON.parse(<string>this.localStorageService.get("userDetails"));
  
  constructor() {
    // this.userDetailsService.userDetailsSource.subscribe({
    //   next: (result) => {
    //     console.log("loading");
        
    //     this.userDetails = result, 
    //     this.isLoaded = true},
    // })
  }

  
}
