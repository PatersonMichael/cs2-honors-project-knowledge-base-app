import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomenavComponent } from '../homenav/homenav.component';

@Component({
  selector: 'app-about',
  standalone: true,
  imports: [CommonModule, HomenavComponent],
  template: `
    <app-homenav></app-homenav>
    <h1>About</h1>
    <article class="about-text">
      <p>
        This is an honors project authored by Michael Paterson, and mentored by Las Positas College computer science professor and department coordinator Carlos Moreno.
      </p>
      <p>
        The goal of the app was to create a full stack application that can store and organize information gathered from various sources of knowledge, such
        as books, articles, multi-media and works of art.
      </p>
      <p>
        The project is made with C# via the DotNet framework, Microsoft SQL Server for data persistence, and Angular for the User Interface.
      </p>
      <p>The project is featured on Github, view <a href="https://github.com/PatersonMichael/cs2-honors-project-knowledge-base-app" target="_blank">here</a></p>
    </article>
  `,
  styleUrl: './about.component.css'
})
export class AboutComponent {

}
