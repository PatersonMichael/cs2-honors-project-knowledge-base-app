import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { AppCookieService } from './app-cookie.service';
import { IExcerptCard } from '../models/IExcerptCard';

@Injectable({
  providedIn: 'root'
})
export class ExcerptService {
  private apiUrl = environment.apiUrl;
  private cookieService = inject(AppCookieService);

  // GET
  async getExcerptsAsync() {
    const response = await fetch(`${this.apiUrl}/api/excerptcards`, {
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

  async getExcerptAsync(id: number) {
    const response = await fetch(`${this.apiUrl}/api/excerptcards/${id}`, {
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
  // POST
  async postExcerptAsync(excerpt: IExcerptCard) {
    const response = await fetch(`${this.apiUrl}/api/excerptcards`, {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      headers: {
        "Content-Type": "application/json",
        "Authorization": this.cookieService.get("Authorization")
      },
      redirect: "follow",
      referrerPolicy: "no-referrer",
      body: JSON.stringify(excerpt),
    });

    return response.json();
  }

  // PUT

  async putExcerptAsync(excerpt: IExcerptCard) {
    const response = await fetch(`${this.apiUrl}/api/excerptcards/${excerpt.excerptCardId}`, {
      method: "PUT",
      mode: "cors",
      cache: "no-cache",
      headers: {
        "Content-Type": "application/json",
        "Authorization": this.cookieService.get("Authorization")
      },
      redirect: "follow",
      referrerPolicy: "no-referrer",
      body: JSON.stringify(excerpt),
    });

    return response.json();
  }

  // DELETE
  async deleteExcerptAsync(id: number) {
    const response = await fetch(`${this.apiUrl}/api/excerptcards/${id}`, {
      method: "DELETE",
      mode: "cors",
      cache: "no-cache",
      headers: {
        "Content-Type": "application/json",
        "Authorization": this.cookieService.get("Authorization")
      },
      redirect: "follow",
      referrerPolicy: "no-referrer",
    })
  }
}
