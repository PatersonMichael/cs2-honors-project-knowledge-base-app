import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../services/user.service';
import { INoteCard } from '../../models/INotecard';

@Component({
  selector: 'app-note-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './note-details.component.html',
  styleUrl: './note-details.component.css'
})
export class NoteDetailsComponent {
  route = inject(ActivatedRoute);
  userService = inject(UserService);
  noteCard: INoteCard | undefined;

  constructor() {
    const noteCardId = parseInt(this.route.snapshot.params['id'], 10);
  }

}
