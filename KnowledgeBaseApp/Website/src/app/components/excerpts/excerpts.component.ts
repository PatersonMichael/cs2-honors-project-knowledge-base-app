import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExcerptCardComponent } from '../excerpt-card/excerpt-card.component';
import { LocalStorageService } from '../../services/local-storage.service';
import { UserDetailsService } from '../../services/user-details.service';
import { IUserProfile } from '../../models/IUserProfile';
import { IExcerptCard } from '../../models/IExcerptCard';
import { ExcerptService } from '../../services/excerpt.service';

@Component({
  selector: 'app-excerpts',
  standalone: true,
  imports: [CommonModule, ExcerptCardComponent],
  template: `
  @defer () {
    <div class="excerpts-component">
      <section class="excerpts">
       <app-excerpt-card
        *ngFor="let excerptCard of excerptCards"
        [excerptCard]="excerptCard"
       ></app-excerpt-card>
      </section>
  
    </div>

  } @loading () {
    <p>Loading Excerpts . . .</p>
  }
  `,
  styleUrl: './excerpts.component.css'
})
export class ExcerptsComponent {
  private userDetailsService = inject(UserDetailsService);
  private localStorageService = inject(LocalStorageService);
  private userDetails: IUserProfile = JSON.parse(<string>this.localStorageService.get("userDetails"));
  private excerptService = inject(ExcerptService);

  excerptCards: IExcerptCard[] = [];

  constructor() {
    this.excerptService.getExcerptsAsync().then(result => {
      this.excerptCards = result;
    })
  }

}
