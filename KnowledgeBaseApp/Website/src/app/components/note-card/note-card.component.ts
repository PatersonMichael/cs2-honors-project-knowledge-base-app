import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { INoteCard } from '../../models/INotecard';
import { RouterLink, RouterModule } from '@angular/router';

@Component({
  selector: 'app-note-card',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterModule],
  template: `
    <div class="note-card">
    <article class="note-card-body">
      <div class="note-card-heading">
        <h2 class="note-card-title">{{noteCard.title}}</h2>
        <a [routerLink]="['/note', noteCard.noteId]">
          <img src="assets/edit-icon-black.png" alt="Edit" class="edit-icon">
      </a>
      </div>
      <p class="note-text">
        {{noteCard.body!.slice(0, 100)}}

        @if(noteCard.body!.length > 100) {
          <span>. . .</span>
        }
      </p>
    </article>
  </div>
  `,
  styleUrl: './note-card.component.css'
})
export class NoteCardComponent {
  @Input() noteCard!: INoteCard;

  // need logic to limit size of note body to 15 words

  noteBodyText: string = '';

  constructor() {
    // this.limitTextBodyLength(this.noteCard.body.split(' '), 15);
  }

  limitTextBodyLength(textBodyWords: string[], maxLength: number) {
    if (textBodyWords.length > maxLength) {
      for (let i = 0; i < textBodyWords.length; i++) {
        this.noteBodyText += textBodyWords[i] + " ";
      }
    }
    else {
      this.noteBodyText = textBodyWords.join(' ');
    }
  }
}
