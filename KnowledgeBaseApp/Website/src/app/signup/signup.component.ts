import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomenavComponent } from '../homenav/homenav.component';
import { FormGroup, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [CommonModule, HomenavComponent, ReactiveFormsModule],
  template: `
    <app-homenav></app-homenav>
    <section class="signup">
      <h2>Sign Up</h2>
      <form (submit)="submitSignupForm()" [formGroup]="signupForm">
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
          <label for="password">Password <span>lowercase, uppercase, number, symbol</span></label>
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
  private passRegex: RegExp = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{12,256}$/;

  signupForm = new FormGroup({
    firstName: new FormControl('', Validators.compose([Validators.required, Validators.maxLength(100)])),
    lastName: new FormControl('', Validators.compose([Validators.required, Validators.maxLength(100)])),
    email: new FormControl('', Validators.compose([Validators.required, Validators.email])),
    password: new FormControl('', Validators.compose([Validators.required, Validators.pattern(this.passRegex)])),
    nametag: new FormControl('', Validators.compose([Validators.required, Validators.maxLength(60)])),
    birthDate: new FormControl('', Validators.required),
  });

  submitSignupForm() {
    // validate form
    // if invalid, show user which data is invalid
    // if valid, send post user request through user-service
      // if 201 response, redirect to user home
          // else, try form submission again

    console.log(
      `Form submitted: 
      \n firstName: ${this.signupForm.value.firstName}
      \n lastName: ${this.signupForm.value.lastName}
      \n email: ${this.signupForm.value.email}
      \n password: ${this.signupForm.value.password}
      \n birthDate: ${this.signupForm.value.birthDate}`
    )
  }
}
