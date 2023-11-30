import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomenavComponent } from '../homenav/homenav.component';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { IUserLoginCredentials } from '../../models/IUserProfile';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, HomenavComponent, ReactiveFormsModule],
  template: `
    <app-homenav [isOnLoginPage]="true"></app-homenav>
    @if (!loginSuccess) {
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
  
          <div class="input-block" id="submit-block">
            <button class="submit-button" [disabled]="!loginForm.valid">Log In</button>
          </div>
        </form>
      </section>
    }
    @else {
      <section class="login-success">
        <p>Success! Loading Profile</p>
      </section>
    }
  `,
  styleUrl: './login.component.css'
})
export class LoginComponent {
  userProfileService = inject(UserService);

  loginForm = new FormGroup({
    email: new FormControl('', Validators.compose([Validators.required, Validators.email])),
    password: new FormControl('', Validators.required),
  })

  isSubmitting: boolean = false;
  invalidCredentials: boolean = false;
  loginSuccess: boolean = false;

  submitLoginForm() {
    this.loginForm.disable();
    this.isSubmitting = true;

    let userCreds: IUserLoginCredentials = {
      email: this.loginForm.value.email,
      password: this.loginForm.value.password,
    }

    let response = this.userProfileService.loginUserProfileAsync(userCreds);
    response.then(result => {
      if (result['statusCode'] == 200) {
        this.loginSuccess = true;
      }
      else {
        this.loginSuccess = false;
        this.invalidCredentials = true;
      }
    })

    console.log(
      `login form submitted:
      \n email: ${this.loginForm.value.email}
      \n password: ${this.loginForm.value.password}
      `
    )

    if (this.loginForm.valid) {
      this.loginForm.disable();
      this.isSubmitting = true;
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
