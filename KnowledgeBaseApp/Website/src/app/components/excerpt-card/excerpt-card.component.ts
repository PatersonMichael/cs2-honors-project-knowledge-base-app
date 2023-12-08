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
        <a [routerLink]="['/excerpt', excerptCard.excerptCardId]">
          <img src="assets/edit-icon-black.png" alt="Edit" class="edit-icon">
      </a>
      </div>
      <p class="excerpt-text">
        {{excerptCard.excerpt!.slice(0, 100)}}

        @if(excerptCard.excerpt!.length > 100) {
          <span>. . .</span>
        }
      </p>
        <p id="excerpt-author">- {{excerptCard.citation?.sourceMaterial?.authorFirstName}} {{excerptCard.citation?.sourceMaterial?.authorLastName}}</p>
    </article>
  </div>
  `,
  styleUrl: './excerpt-card.component.css'
})
export class ExcerptCardComponent {
  @Input() excerptCard!: IExcerptCard;

  excerptText: string = '';

  constructor() {
  }
}
