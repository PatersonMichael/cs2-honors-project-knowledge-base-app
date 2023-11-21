import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomenavComponent } from '../homenav/homenav.component';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, HomenavComponent, ReactiveFormsModule],
  template: `
    <app-homenav [isOnLoginPage]="true"></app-homenav>
    <section class="login">
      <h2>Log In</h2>
      <form [formGroup]="loginForm" (submit)="submitLoginForm()">
        @if (isSubmitting) {
            <p id="is-submitting">Submitted!</p>
          }
          @else if (invalidCredentials) {
            <p id="invalid-credentials">Invalid Credentials</p>
  
          }
        <div class="input-block">
          <label for="email">Email</label>
          <input type="email" id="email" formControlName="email">
        </div>

        <div class="input-block">
          <label for="password">Password</label>
          <input type="password" id="password" formControlName="password">
        </div>

        <div id="submit-block">
          <button class="submit-button" [disabled]="!loginForm.valid">Log In</button>
        </div>
      </form>
    </section>
  `,
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm = new FormGroup({
    email: new FormControl('', Validators.compose([Validators.required, Validators.email])),
    password: new FormControl('', Validators.required),
  })

  isSubmitting: boolean = false;
  invalidCredentials: boolean = false;

  submitLoginForm() {
    this.loginForm.disable();
    this.isSubmitting = true;
    console.log(
      `login form submitted:
      \n email: ${this.loginForm.value.email}
      \n password: ${this.loginForm.value.password}
      `
    )

    if (this.loginForm.valid) {

    }
    else {
      this.loginForm.enable();
      this.isSubmitting = false;
    }
    // userService.login(loginForm.value.email, loginForm.value.password)

    // if logged in successfully, redirect to user home
      // set invalidCredentials = false
    // else, set invalidCredentials to true
    // reset form, reenable form

  }
}
