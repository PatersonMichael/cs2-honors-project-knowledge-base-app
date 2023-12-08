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

  // excerptCards: IExcerptCard[] = [
  //   {
  //     excerptCardId: 1,
  //     title: "Grubles are better than scruples",
  //     excerpt: "Everyone Knows that the sages of old cherished gruples, but looked down upon scruples.",
  //     creationDate: new Date(),
  //     lastUpdatedDate: new Date(),
  //     userProfileId: 8,
  //     citationId: 1,
  //     citation: {
  //       citationId: 1,
  //       format: "MLA",
  //       excerptLocation: "page 11",
  //       creationDate: new Date(),
  //       userProfileId: 8,
  //       sourceMaterialId: 1,
  //       sourceMaterial: {
  //         sourceMaterialId: 1,
  //         title: "History of Grubles: Vol. 28",
  //         publishDate: new Date("1800-12-8"),
  //         publisher: "Doblton Publishing",
  //         sourceMaterialType: "Book",
  //         sourceMaterialEdition: "1",
  //         authorFirstName: "Grubbly",
  //         authorLastName: "Dobblers",
  //         userProfileId: 1
  //       }
  //     }
  //   },
  //   {
  //     excerptCardId: 1,
  //     title: "Grubles are better than scruples",
  //     excerpt: "Everyone Knows that the sages of old cherished gruples, but looked down upon scruples.",
  //     creationDate: new Date(),
  //     lastUpdatedDate: new Date(),
  //     userProfileId: 8,
  //     citationId: 1,
  //     citation: {
  //       citationId: 1,
  //       format: "MLA",
  //       excerptLocation: "page 11",
  //       creationDate: new Date(),
  //       userProfileId: 8,
  //       sourceMaterialId: 1,
  //       sourceMaterial: {
  //         sourceMaterialId: 1,
  //         title: "History of Grubles: Vol. 28",
  //         publishDate: new Date("1800-12-8"),
  //         publisher: "Doblton Publishing",
  //         sourceMaterialType: "Book",
  //         sourceMaterialEdition: "1",
  //         authorFirstName: "Grubbly",
  //         authorLastName: "Dobblers",
  //         userProfileId: 1
  //       }
  //     }
  //   },
  //   {
  //     excerptCardId: 1,
  //     title: "Grubles are better than scruples",
  //     excerpt: "Everyone Knows that the sages of old cherished gruples, but looked down upon scruples.",
  //     creationDate: new Date(),
  //     lastUpdatedDate: new Date(),
  //     userProfileId: 8,
  //     citationId: 1,
  //     citation: {
  //       citationId: 1,
  //       format: "MLA",
  //       excerptLocation: "page 11",
  //       creationDate: new Date(),
  //       userProfileId: 8,
  //       sourceMaterialId: 1,
  //       sourceMaterial: {
  //         sourceMaterialId: 1,
  //         title: "History of Grubles: Vol. 28",
  //         publishDate: new Date("1800-12-8"),
  //         publisher: "Doblton Publishing",
  //         sourceMaterialType: "Book",
  //         sourceMaterialEdition: "1",
  //         authorFirstName: "Grubbly",
  //         authorLastName: "Dobblers",
  //         userProfileId: 1
  //       }
  //     }
  //   },
  // ]



  constructor() {
    this.excerptService.getExcerptsAsync().then(result => {
      this.excerptCards = result;
    })
  }

}
