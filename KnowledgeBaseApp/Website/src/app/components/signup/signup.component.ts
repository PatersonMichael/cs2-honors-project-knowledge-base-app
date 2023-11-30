import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomenavComponent } from '../homenav/homenav.component';
import { FormGroup, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { IUserLoginCredentials, IUserProfile } from '../../models/IUserProfile';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [CommonModule, HomenavComponent, ReactiveFormsModule],
  template: `
    <app-homenav></app-homenav>
    <section class="signup">
      <h2>Sign Up</h2>
      <form (submit)="submitSignupForm()" [formGroup]="signupForm">
        @if (isSubmitting) {
          <p id="is-submitting">Submitted!</p>
        }
        <div class="input-block">
          <label for="first-name">First Name</label>
          <input type="text" id="first-name" formControlName="firstName">
        </div>

        <div class="input-block">
          <label for="last-name">Last Name</label>
          <input type="text" id="last-name" formControlName="lastName">
        </div>

        <div class="input-block">
          <label for="email">Email</label>
          <input type="text" id="email" formControlName="email">
        </div>

        <div class="input-block">
          <label for="password">Password <span>min 9 characters, lowercase, uppercase, number, symbol</span></label>
          <input type="password" id="password" formControlName="password">
        </div>

        <div class="input-block">
          <label for="nametag">Nametag</label>
          <input type="text" id="nametag" formControlName="nametag" placeholder="@">
        </div>

        <div class="input-block" id="birth-date-block">
          <label class="birth-date-label" for="birth-date">Birth Date</label>
          <input class="birth-date-input" type="date" id="birth-date" formControlName="birthDate">
        </div>

        <div class="input-block" id="submit-block">
          <button class="submit-button" type="submit" [disabled]="!signupForm.valid">Submit</button>
        </div>
      </form>
    </section>
  `,
  styleUrl: './signup.component.css'
})
export class SignupComponent {
  userProfileService = inject(UserService);

  private passRegex: RegExp = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{9,256}$/;
  isSubmitting: boolean = false;

  signupForm = new FormGroup({
    firstName: new FormControl('', Validators.compose([Validators.required, Validators.maxLength(100)])),
    lastName: new FormControl('', Validators.compose([Validators.required, Validators.maxLength(100)])),
    email: new FormControl('', Validators.compose([Validators.required, Validators.email])),
    password: new FormControl('', Validators.compose([Validators.required, Validators.pattern(this.passRegex)])),
    nametag: new FormControl('', Validators.compose([Validators.required, Validators.maxLength(60)])),
    birthDate: new FormControl('', Validators.required),
  });

  submitSignupForm() {
    this.signupForm.disable();
    this.isSubmitting = true;
    let userProfile: IUserProfile = {
      firstName: this.signupForm.value.firstName,
      lastName: this.signupForm.value.lastName,
      email: this.signupForm.value.email,
      password: this.signupForm.value.password,
      nametag: this.signupForm.value.nametag,
      birthDate: this.signupForm.value.birthDate,
      creationDate: new Date(),
    }
    // validate form
    // if valid, send post user request through user-service
    const response = this.userProfileService.postUserProfileAsync(userProfile).then(result => {
        console.log(result);
        if (result['userProfileId'] > 0) {
          let userLoginCredentials: IUserLoginCredentials = {
            email: this.signupForm.value.email,
            password: this.signupForm.value.password
          }
          this.userProfileService.loginUserProfileAsync(userLoginCredentials);
        }
    });
      // if 201 response, login with new user credentials (email/pass) and redirect to user home
          // else, try form submission again, indicate problems with validation
            // isSubmitting = false;

    console.log(
      `Form submitted: 
      \n firstName: ${this.signupForm.value.firstName}
      \n lastName: ${this.signupForm.value.lastName}
      \n email: ${this.signupForm.value.email}
      \n password: ${this.signupForm.value.password}
      \n nametag: ${this.signupForm.value.nametag}
      \n birthDate: ${this.signupForm.value.birthDate}
      \n\n API response:
      \n ${response}
      `
    )
    // console.log(response);
  }
}
