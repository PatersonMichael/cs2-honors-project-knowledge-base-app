import { Injectable, inject} from '@angular/core';
import { TimeUtilities } from '../../utils/timeUtilities';

/*
  Handles parsing, storage, access, and removal
  of cookies for the application.

*/
@Injectable({
  providedIn: 'root'
})
export class AppCookieService {
  private cookieStore: any  = {};

  constructor() { 
    this.parseCookies(document.cookie);
  }

  public parseCookies(cookies = document.cookie) {
    this.cookieStore = {};

    // guard against null/undefined
    if (!!cookies === false) {
      return;
    }
    // get cookies from browser. delimited by ';'
    const cookiesArr = cookies.split(';');
    for (const cookie of cookiesArr) {
      const cookieArr = cookie.split('=');
      this.cookieStore[cookieArr[0].trim()] = cookieArr[1];
    }
  }

  get(key: string) {
    this.parseCookies();
    return !!this.cookieStore[key] ? this.cookieStore[key] : null;
  }

  remove(key: string, path?: string ) {
    document.cookie = `${key} = ; expires=Thu, 1 jan 1990 12:00:00 UTC; path=/`;
    document.cookie = `${key} = ; expires=Thu, 1 jan 1990 12:00:00 UTC; path=/user}`;
    document.cookie = `${key} = ; expires=Thu, 1 jan 1990 12:00:00 UTC; path=${path}}`;

    // document.cookie = `${key} = ;`;
  }

  set(key:string, value: string) {
    let date = new Date();
    // console.log(TimeUtilities.addHours(date, 1).toUTCString());
    document.cookie = key + '=' + (value || '') + ";" + "expires=" + TimeUtilities.addHours(date, 2).toUTCString() + ";path='/'";
    // document.cookie = `${key}=${value || ''}; expires=${TimeUtilities.addHours(date, 1).toUTCString()}; httpOnly=true; `;
  }
}
