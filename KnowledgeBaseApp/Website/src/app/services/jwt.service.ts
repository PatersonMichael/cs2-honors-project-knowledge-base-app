import { Injectable } from '@angular/core';


 // Handles jwt storage and access
 /*
 need token interceptor to supply outgoing requests
 with Authorization header. interceptor could use
 getToken() from this service.
 */
@Injectable({
  providedIn: 'root'
})
export class JwtService {

  // function for recieving jwt and storing it
    // cookie or local storage?

  // function for getting jwt

  // storeToken()

  // getToken

  constructor() { }
}
