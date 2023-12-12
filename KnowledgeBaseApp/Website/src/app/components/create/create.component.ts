import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterOutlet } from '@angular/router';
import { NoteService } from '../../services/note.service';
import { INoteCard } from '../../models/INotecard';
import { LocalStorageService } from '../../services/local-storage.service';
import { NewNoteComponent } from '../new-note/new-note.component';
import { NewExcerptComponent } from '../new-excerpt/new-excerpt.component';
import { OPTION } from '../../enums/option';

@Component({
  selector: 'app-create',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterOutlet,
      NewNoteComponent, NewExcerptComponent],
  template: `
    <section class="create">
      @if (!choiceMade) {
        <h1>What would you like to Create?</h1>
        <div class="options">
          <button class="option" [disabled]="choiceMade" (click)="select('note')">Note</button>
          <button class="option" [disabled]="choiceMade" (click)="select('excerpt')">Excerpt</button>
        </div>
      }
      @if (creatingNote) {
        <app-new-note (isCancelling)="cancel()"></app-new-note>
      } 
      @else if (creatingExcerpt) {
        <app-new-excerpt (isCancelling)="cancel()"></app-new-excerpt>
      }
    </section>
  `,
  styleUrl: './create.component.css'
})
export class CreateComponent {

  // will support creation of either notes or excerpts
  choiceMade: boolean = false;
  creatingNote: boolean = false;
  creatingExcerpt: boolean = false;

  // dependencies
  private noteService = inject(NoteService);
  private localStorageService = inject(LocalStorageService)

  // methods
  select(option: string) {
    if (option === 'note') {
      this.choiceMade = true;
      this.creatingNote = true;
    }
    else if (option === 'excerpt') {
      this.choiceMade = true;
      this.creatingExcerpt = true;
    }
  }

  cancel() {
    this.choiceMade = false;
    this.creatingNote = false;
    this.creatingExcerpt = false;
  }

  
}
