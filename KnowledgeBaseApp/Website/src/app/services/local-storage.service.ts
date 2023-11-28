import { Injectable } from '@angular/core';


// if cookieService doesn't work, this is a more basic alternative
// sets, accesses, or removes items from localStorage in the browser
@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  constructor() { }

  get(key: string) {
    return localStorage.getItem(key);
  }

  set(key: string, value: string) {
    localStorage.setItem(key, value);
  }

  remove(key: string) {
    localStorage.removeItem(key);
  }
}
