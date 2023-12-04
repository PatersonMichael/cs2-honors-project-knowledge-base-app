import { Component, Input, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserNavComponent } from '../user-nav/user-nav.component';
import { IUserProfile } from '../../models/IUserProfile';
import { UserDetailsService } from '../../services/user-details.service';
import { LocalStorageService } from '../../services/local-storage.service';
import { INoteCard } from '../../models/INotecard';
import { NoteCardComponent } from '../note-card/note-card.component';

@Component({
  selector: 'app-notes',
  standalone: true,
  imports: [CommonModule, NoteCardComponent],
  template: `
      <div class="notes-component">
        <section class="notes">
         <app-note-card
          *ngFor="let noteCard of noteCards"
          [noteCard]="noteCard"
         ></app-note-card>
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
  noteCards: INoteCard[] = [
    {
      noteId: 1,
      title: "My Note",
      body: "test",
      keywords: [],
      creationDate: new Date(),
      lastUpdateDate: new Date,
      userProfileId: 1,

    },
    {
      noteId: 2,
      title: "My Note",
      body: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Iusto, rem! Quisquam quasi consequatur fugit deserunt dignissimos corporis sed perspiciatis error, aut officia. Et ea sint voluptas suscipit id animi temporibus, ipsam culpa cum quaerat nisi soluta. Dolore veritatis rem voluptatem quis magni soluta, reiciendis nemo sequi deserunt quisquam doloribus hic rerum? Tenetur praesentium maxime dolores rerum ut quidem cupiditate quibusdam",
      keywords: [],
      creationDate: new Date(),
      lastUpdateDate: new Date,
      userProfileId: 1,

    },
    {
      noteId: 3,
      title: "My Note",
      body: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Iusto, rem! Quisquam quasi consequatur fugit deserunt dignissimos corporis sed perspiciatis error, aut officia. Et ea sint voluptas suscipit id animi temporibus, ipsam culpa cum quaerat nisi soluta. Dolore veritatis rem voluptatem quis magni soluta, reiciendis nemo sequi deserunt quisquam doloribus hic rerum? Tenetur praesentium maxime dolores rerum ut quidem cupiditate quibusdam",
      keywords: [],
      creationDate: new Date(),
      lastUpdateDate: new Date,
      userProfileId: 1,

    },
    {
      noteId: 4,
      title: "My Note",
      body: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Iusto, rem! Quisquam quasi consequatur fugit deserunt dignissimos corporis sed perspiciatis error, aut officia. Et ea sint voluptas suscipit id animi temporibus, ipsam culpa cum quaerat nisi soluta. Dolore veritatis rem voluptatem quis magni soluta, reiciendis nemo sequi deserunt quisquam doloribus hic rerum? Tenetur praesentium maxime dolores rerum ut quidem cupiditate quibusdam",
      keywords: [],
      creationDate: new Date(),
      lastUpdateDate: new Date,
      userProfileId: 1,

    },

  ]
  
  constructor() {
    // this.userDetailsService.userDetailsSource.subscribe({
    //   next: (result) => {
    //     console.log("loading");
        
    //     this.userDetails = result, 
    //     this.isLoaded = true},
    // })
  }

  
}
