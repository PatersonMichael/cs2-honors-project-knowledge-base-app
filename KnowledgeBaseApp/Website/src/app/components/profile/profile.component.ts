import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { LocalStorageService } from '../../services/local-storage.service';
import { IUserLoginCredentials, IUserProfile } from '../../models/IUserProfile';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <section id="change-password">
      @if (success) {
        <p id="success">Success!</p>
      }
      @else if (invalidCred) {
        <p class="invalid-credentials">Invalid Credentials</p>
      }
      @else if (samePass) {
        <p class="invalid-credentials">Passwords Match</p>
      }
      <button (click)="changingPass = true" [disabled]="changingPass" [hidden]="changingPass">Change Password</button>
      @if (changingPass) {
        <form [formGroup]="passwordForm" id="password-form">
        <label for="old-password">Old Password</label>
        <input type="password" id="old-password" formControlName="oldPassword">
        <label for="password">New Password</label>
        <input type="password" id="password" formControlName="password">
        <button id="submit-button" (click)="submitNewPass()">Submit</button>
      </form>
      }
    </section>
  `,
  styleUrl: './profile.component.css'
})
export class ProfileComponent {
private userService = inject(UserService);
private localStorageService = inject(LocalStorageService);

private userDetails: IUserProfile = JSON.parse(<string>this.localStorageService.get("userDetails"));
private passRegex: RegExp = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{9,256}$/;

userProfile: IUserProfile = this.userDetails;


changingPass: boolean = false
success: boolean = false;
invalidCred: boolean = false;
samePass: boolean = false;

passwordForm = new FormGroup({
  password: new FormControl('', Validators.compose([Validators.required, Validators.pattern(this.passRegex)])),
  oldPassword: new FormControl('', Validators.required),
})

submitNewPass() {
  if (this.passwordForm.valid) {
    if (this.passwordForm.value.oldPassword == this.passwordForm.value.password) {
      this.samePass = true;
      this.changingPass = false;

      return;
    }
    let userLoginCreds: IUserLoginCredentials = {
      email: this.userDetails.email,
      password: this.passwordForm.value.oldPassword
    }

    this.userProfile.password = this.passwordForm.value.password;
    this.userService.loginUserProfileNoNavAsync(userLoginCreds).then(result => {
      if (result == 200) {
        this.userService.putUserProfileAsync(this.userProfile);
      }
      else {
        this.invalidCred = true;
        this.changingPass = false;
      }
    });

  }
}
}
