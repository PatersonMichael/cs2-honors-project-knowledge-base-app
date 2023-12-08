import { Injectable, inject } from '@angular/core';
import { IUserProfile, IUserLoginCredentials } from '../models/IUserProfile';
import { environment } from '../../environments/environment.development';
import { LocalStorageService } from './local-storage.service';
import { AppCookieService } from './app-cookie.service';
import { Router } from '@angular/router';
import { getErrorMessage } from '../../utils/errorUtilities';

// Where Http transactions are made with api

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = environment.apiUrl;
  private localStorageService = inject(LocalStorageService);
  private cookieService = inject(AppCookieService);
  private router = inject(Router);
  // GET

  async getUserProfileAsync() {
    // -1 makes API search for UserProfileId in auth token
   const response = await fetch(`${this.apiUrl}/api/UserProfiles/-1`, {
     method: "GET",
     mode: "cors",
     cache: "no-cache",
     headers: {
       "Content-Type": "application/json",
       "Authorization": this.cookieService.get("Authorization"),
     },
     redirect: "follow",
     referrerPolicy: "no-referrer",
   });

   return response.json();

  }

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
    try {
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

      const responseJson = response.json();
  
      responseJson.then(result => {
        if(result['statusCode'] == 200) {
          // console.log("try storing as key");
          // this.localStorageService.set("Authorization", `Bearer ${result.value}`)
          // console.log(this.localStorageService.get("Authorization"));
  
          this.cookieService.set("Authorization", `Bearer ${result.value}`);
          // console.log(this.cookieService.get("Authorization"));
          this.router.navigate(['/user']);
          this.getUserProfileAsync().then(result => {
            this.localStorageService.add<IUserProfile>("userDetails", result);
          })
        }
      });
      

      return responseJson;
      
    } catch (error) {
      reportError({message: getErrorMessage(error)});
    }

    // if statuscode is 200, store jwt
  }

  // takes path to authorzation cookie and deletes it. 
  public logout(path?: string) {
    this.cookieService.remove("Authorization", path);
    this.localStorageService.remove("Authorization");
    this.localStorageService.remove("userDetails");
    if (this.cookieService.get("Authorization")) {
      console.log("cookie found");
      
    }
  }

  // PUT
  // putUserProfileAsync()

  // DELETE
  // deleteUserProfileAsync()
  constructor() { }
}
