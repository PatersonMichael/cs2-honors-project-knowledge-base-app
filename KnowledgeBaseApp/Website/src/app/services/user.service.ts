import { Injectable } from '@angular/core';
import { IUserProfile, IUserLoginCredentials } from '../models/IUserProfile';
import { environment } from '../../environments/environment.development';
import { FormGroup } from '@angular/forms';

// Where Http transactions are made with api

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = environment.apiUrl;

  // GET
  // getUserProfileAsync()

  // POST
  async postUserProfileAsync(userProfile: IUserProfile) {
    const response = await fetch(`${this.apiUrl}/api/UserProfiles`, {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      headers: {
        "Content-Type": "application/json",
      },
      redirect: "follow",
      referrerPolicy: "no-referrer",
      body: JSON.stringify(userProfile),
    });

    return response.json();
  }

  async loginUserProfileAsync(loginCreds: IUserLoginCredentials) {
    const response = await fetch (`${this.apiUrl}/api/Authentication`, {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      credentials: "same-origin",
      headers: {
        "Content-Type": "application/json",
      },
      redirect: "follow",
      referrerPolicy: "no-referrer",
      body: JSON.stringify(loginCreds),
    });

    return response.json();
  }

  // PUT
  // putUserProfileAsync()

  // DELETE
  // deleteUserProfileAsync()
  constructor() { }
}
