import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { INoteCard } from '../../models/INotecard';
import { UserNavComponent } from '../user-nav/user-nav.component';
import { NoteService } from '../../services/note.service';


@Component({
  selector: 'app-note-details',
  standalone: true,
  imports: [CommonModule, UserNavComponent],
  template: `
    <app-user-nav></app-user-nav>
    <section class="note-details-page">
      <section id="note">
        <div id="note-heading">
          <h1 id="note-title" [textContent]="noteCard?.title" [contentEditable]="editMode" (input)="onTitleChangeUpdate($any($event).target.innerHTML)"></h1>
          <img (click)="editMode = true" src="assets/edit-icon.png" alt="Edit">
        </div>
        <article id="note-body" [contentEditable]="editMode" [textContent]="noteBody" (input)="onBodyChangeUpdate($any($event).target.innerHTML)">
        </article>
        <div id="footer">
          <button id="delete-button" [disabled]="!editMode"
           (click)="wantsToDelete = true"  [hidden]="!editMode">Delete</button>
          <button id="save-changes-button" [disabled]="!editMode"
           (click)="saveChanges()"  [hidden]="!editMode">Save Changes</button>
          @if (wantsToDelete) {
            <article id="check-delete">
              <p>Are you sure you want to delete this note?</p>
              <div id="choice-buttons">
                <button id="yes-button" (click)="deleteNote()">Yes</button>
                <button id="no-button" (click)="wantsToDelete = false">No</button>

              </div>
            </article>
          }
        </div>

      </section>
    </section>
  `,
  styleUrl: './note-details.component.css'
})
export class NoteDetailsComponent {
  route = inject(ActivatedRoute);
  router = inject(Router);
  noteService = inject(NoteService);
  noteCard: INoteCard | undefined;

  editMode: boolean = false;
  wantsToDelete: boolean = false;

  noteBody: string = '';
  
  constructor() {
    const noteCardId = parseInt(this.route.snapshot.params['id'], 10);
    this.noteService.getNoteCardAsync(noteCardId).then(result => {
      this.noteCard = result;
      this.noteBody = result['body'];
    });
  }

  log(data: Element) {
    console.log(data);
  }

  onBodyChangeUpdate(data: string | null) {
    if (data) {
      this.noteCard!.body = data;

    }
  }

  onTitleChangeUpdate(data: string | null) {
    if (data) {
      this.noteCard!.title = data;
    }
  }

  saveChanges() {
    this.noteCard!.lastUpdatedDate = new Date();
    this.noteService.putNoteCardAsync(<INoteCard>this.noteCard).then(result => {
      console.log(result);
      
    })
  }

  deleteNote() {
    this.noteService.deleteNoteCardAsync(<number>this.noteCard!.noteId);
    setTimeout(() => {this.router.navigate(['user/notes'])}, 1000);
    // this.router.navigate(['user/notes']);
  }

}
