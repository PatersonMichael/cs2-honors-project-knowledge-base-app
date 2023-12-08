import { Injectable, inject } from '@angular/core';
import { INoteCard } from '../models/INotecard';
import { environment } from '../../environments/environment.development';
import { AppCookieService } from './app-cookie.service';


// handles API requests for note cards
@Injectable({
  providedIn: 'root'
})
export class NoteService {
  private apiUrl = environment.apiUrl;
  private cookieService = inject(AppCookieService);

  // GET
  async getNoteCardsAsync() {
    const response = await fetch(`${this.apiUrl}/api/notes`, {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      headers: {
        "Content-Type": "application/json",
        "Authorization": this.cookieService.get("Authorization")
      },
      redirect: "follow",
      referrerPolicy: "no-referrer"
    });

    return response.json();
  }

  async getNoteCardAsync(id: number) {
    const response = await fetch(`${this.apiUrl}/api/notes/${id}`, {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      headers: {
        "Content-Type": "application/json",
        "Authorization": this.cookieService.get("Authorization")
      },
      redirect: "follow",
      referrerPolicy: "no-referrer"
    })

    return response.json();
  }

  // POST
  async postNoteCardAsync(noteCard: INoteCard) {
    const response = await fetch(`${this.apiUrl}/api/notes`, {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      headers: {
        "Content-Type": "application/json",
        "Authorization": this.cookieService.get("Authorization")
      },
      redirect: "follow",
      referrerPolicy: "no-referrer",
      body: JSON.stringify(noteCard),
    });

    return response.json();
  }

  // PUT
  async putNoteCardAsync(noteCard: INoteCard) {
    const response = await fetch(`${this.apiUrl}/api/notes/${noteCard.noteId}`, {
      method: "PUT",
      mode: "cors",
      cache: "no-cache",
      headers: {
        "Content-Type": "application/json",
        "Authorization": this.cookieService.get("Authorization")
      },
      redirect: "follow",
      referrerPolicy: "no-referrer",
      body: JSON.stringify(noteCard),
    });

    return response.json();
  }

  // DELETE
  async deleteNoteCardAsync(id: number) {
    const response = await fetch(`${this.apiUrl}/api/notes/${id}`, {
      method: "DELETE",
      mode: "cors",
      cache: "no-cache",
      headers: {
        "Content-Type": "application/json",
        "Authorization": this.cookieService.get("Authorization")
      },
      redirect: "follow",
      referrerPolicy: "no-referrer",
    });
  }
  
  constructor() { }
}
