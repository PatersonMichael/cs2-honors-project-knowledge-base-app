import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserNavComponent } from '../user-nav/user-nav.component';

@Component({
  selector: 'app-notes',
  standalone: true,
  imports: [CommonModule, UserNavComponent],
  template: `
  <div class="notes-component">
    <section class="notes">
      <h1>Notes</h1>
  
    </section>

  </div>
  `,
  styleUrl: './notes.component.css'
})
export class NotesComponent {

}
