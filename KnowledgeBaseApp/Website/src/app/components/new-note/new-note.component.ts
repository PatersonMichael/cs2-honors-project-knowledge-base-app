import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { INoteCard } from '../../models/INotecard';
import { LocalStorageService } from '../../services/local-storage.service';
import { NoteService } from '../../services/note.service';
import { IUserProfile } from '../../models/IUserProfile';
import { IKeyword } from '../../models/IKeyword';
import { Router } from '@angular/router';


@Component({
  selector: 'app-new-note',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <section id="note">
      <form [formGroup]="noteForm">
        <header>
          <h1><input id="note-title" type="text" placeholder="Untitled Note" formControlName="title"></h1>
        </header>
        <article id="body-input-area">
          <textarea type="text" id="body-input" placeholder="Enter your notes here" formControlName="body"></textarea>
        </article>
      </form>
        <article id="keyword-area">
          <form [formGroup]="keywordForm">
            <label for="keywords">Add Keywords Here</label>
            <input type="text" id="keywords" formControlName="word">
            <button id="keyword-button" (click)="addKeyword()">Add Keyword</button>
        </form>
        @if (this.keywordsArr.length > 0) {
          <p id="delete-help">click keywords to delete them</p>
        }
          <ul id="keywords-list">
            @for(keyword of keywordsArr; track $index) {
              <li class="keyword"  (click)="removeKeyword(keyword)">{{keyword}}</li>
            }

          </ul>
        </article>
        <div id="bottom">
          <button id="submit-button" (click)="submitNote()">Save Note</button>
        </div>
    </section>
  `,
  styleUrl: './new-note.component.css'
})
export class NewNoteComponent {
  private localStorageService = inject(LocalStorageService);
  private noteService = inject(NoteService);
  userDetails: IUserProfile = JSON.parse(<string>this.localStorageService.get("userDetails"));
  private router = inject(Router);


  

  noteForm = new FormGroup({
    title: new FormControl('', Validators.compose([Validators.required, Validators.maxLength(200)])),
    body: new FormControl('', Validators.compose([Validators.required]))
  });

 
  submitNote() {
    let noteCard: INoteCard = {
      noteId: 0,
      title: this.noteForm.value.title,
      body: this.noteForm.value.body,
      userProfileId: this.userDetails.userProfileId,
      keywords: this.processKeywords(this.keywordsArr),
      creationDate: new Date(),
      lastUpdateDate: new Date(),
    }

    const response = this.noteService.postNoteCardAsync(noteCard).then(result => {
      if (result['noteId'] > 0) {
        // redirect to created note with associated id
        this.router.navigate([`note/${result['noteId']}`]);
      }
    })
  }

  // Keywords need to be handled in a special way.
  // "add keywords" section allows user to enter strings
  // these strings are added to an array, and for each one add it as
  // an instance of a keyword
  processKeywords(keywords: string[]) {
    let keywordObjects: IKeyword[] = [];
    for(let word of keywords) {
      keywordObjects.push({
        keywordId: 0,
        userProfileId: this.userDetails.userProfileId,
        name: word
      })
    }
     return keywordObjects;
  }

  keywordsEmpty: boolean = true;
  maxKeywordsReached: boolean = false;

  keywordsArr: string[] = [];
  keywordForm = new FormGroup({
    word: new FormControl('', Validators.compose([Validators.required, Validators.maxLength(40)])),
  })
  addKeyword() {
    // search keywords for if this already exists
    for (let i = 0; i < this.keywordsArr.length; i++) {
      if (this.keywordForm.value.word == this.keywordsArr[i]) {
        return;
      }
    }
    if (this.keywordForm.valid) {
      this.keywordsArr.push(this.keywordForm.value.word!);

    }
    this.keywordsEmpty = false;
    if (this.keywordsArr.length == 15) {
      this.maxKeywordsReached = true;
      this.keywordForm.disable();
    }
  }

  removeKeyword(word: string) {
    let newArr: string[] = [];
    for (let i = 0; i < this.keywordsArr.length; i++) {
      if (this.keywordsArr[i] == word) {
      }
      else {
        newArr.push(this.keywordsArr[i]);
      }
    }
    this.keywordsArr = newArr;
    if (this.keywordsArr.length < 15) {
      this.keywordForm.enable();
    }
    if (this.keywordsArr.length == 0) {
      this.keywordsEmpty = true;
    }
  }
}
