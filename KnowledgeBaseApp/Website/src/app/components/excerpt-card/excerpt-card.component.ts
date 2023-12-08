import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IExcerptCard } from '../../models/IExcerptCard';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-excerpt-card',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
  <div class="excerpt-card">
    <article class="excerpt-card-body">
      <div class="excerpt-card-heading">
        <h2 class="excerpt-card-title">{{excerptCard.title}}</h2>
        <a [routerLink]="['/note', excerptCard.excerptCardId]">
          <img src="assets/edit-icon-black.png" alt="Edit" class="edit-icon">
      </a>
      </div>
      <p class="note-text">
        {{excerptCard.excerpt!.slice(0, 100)}}

        @if(excerptCard.excerpt!.length > 100) {
          <span>. . .</span>
        }
      </p>
    </article>
  </div>
  `,
  styleUrl: './excerpt-card.component.css'
})
export class ExcerptCardComponent {
  @Input() excerptCard!: IExcerptCard;

  // need logic to limit size of note body to 15 words

  excerptText: string = '';

  constructor() {
    // this.limitTextBodyLength(this.noteCard.body.split(' '), 15);
  }

  limitTextBodyLength(textBodyWords: string[], maxLength: number) {
    if (textBodyWords.length > maxLength) {
      for (let i = 0; i < textBodyWords.length; i++) {
        this.excerptText += textBodyWords[i] + " ";
      }
    }
    else {
      this.excerptText = textBodyWords.join(' ');
    }
  }
}
