import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomenavComponent } from '../homenav/homenav.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, HomenavComponent],
  template: `
    <app-homenav></app-homenav>
    <section class="hero">
      <div class="hero-text">
        <h1 class="hero-header">Nurture Your Mind</h1>
        <p class="hero-subheader">A common place to store wisdom, creativity, and expertise.</p>
        <section class="demo">
          <article>
            <p class="demo-heading">Store quotes you find inspiring, 
              eye-opening, relatable, or hilarious, as excerpts.
            </p>
            <div class="demo-card">
              <div class="demo-card-title">
                <h2>Success is Vanity</h2>
                <img src="assets/edit-icon.png" alt="">
              </div>
              <p class="demo-card-body">"The poets and sages have, 
                indeed, been saying for centuries 
                that success in this world is vanity."
              </p>
              <p class="demo-author">- Alan Watts</p>
            </div>
          </article>
          <article>
            <p class="demo-heading">Explore your ideas and write them out in notes.
            </p>
            <div class="demo-card">
            <div class="demo-card-title">
                <h2>My Note</h2>
                <img src="assets/edit-icon.png" alt="">
              </div>
              <p class="demo-card-body">Lorem ipsum dolor, sit amet consectetur adipisicing elit. Dignissimos, commodi.
              </p>
            </div>
          </article>
        </section>
        <button class="sign-up-button">Sign Up</button>
      </div>
      <div class="hero-image">
        <img src="/assets/marcus-aurelius-portrait.png" alt="insert cool philosopher here">
      </div>
    </section>
  `,
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
