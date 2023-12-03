import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { IUserProfile } from '../models/IUserProfile';


// Provides a subject for user details that can be cast to multiple observers.
// subscribers to UserDetailService are provided user details data

@Injectable({
  providedIn: 'root'
})
export class UserDetailsService {

  userDetailsSource = new Subject<IUserProfile>();

  setUserDetails(userDetails: IUserProfile) {
    this.userDetailsSource.next(userDetails);
  }

  getUserDetails() {
    this.userDetailsSource.subscribe((x:IUserProfile) => {
      return x;
    });
  }
}
