import { Component, EventEmitter, inject, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';
import { IExcerptCard } from '../../models/IExcerptCard';
import { UserDetailsService } from '../../services/user-details.service';
import { IUserProfile } from '../../models/IUserProfile';
import { LocalStorageService } from '../../services/local-storage.service';
import { ExcerptService } from '../../services/excerpt.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-new-excerpt',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template:`
    <section id="excerpt">
      <form [formGroup]="excerptForm">
        <header>
          <input type="text" id="excerpt-title" placeholder="Untitled Excerpt" formControlName="title">
        </header>
        <article id="excerpt-body">
          <textarea name="" id="excerpt-body-input" placeholder="Type the quote from your source here" formControlName="excerpt"></textarea>
        </article>
        <section id="citation-form-area">
          <h2>Citation</h2>
          <div id="citation-container">
            <article id="citation-form-left">
              <label for="source-title">Source Material Title <span class="required">*</span></label>
              <input type="text" class="long-input" id="source-title" placeholder="Source Title" formControlName="sourceTitle">
              <label for="source-edition">Source Material Edition</label>
              <input type="text" id="source-edition" placeholder="Source Edition" class="long-input" formControlName="sourceEdition">
              <div class="short-inputs">
                <label for="author-first-name">Author First Name <span class="required">*</span></label>
                <input type="text" class="long-input" placeholder="Author First Name" id="author-first-name" formControlName="authorFirstName">
                <label for="author-last-name">Author Last Name</label>
                <input type="text" class="long-input" placeholder="Author Last Name" id="author-last-name" formControlName="authorLastName">
                <label for="quote-location-excerpt">Quote Location (ex: Page Number)</label>
                <input type="text" class="long-input" id="quote-location-input" placeholder="Quote Location (page #, paragraph, etc.)" formControlName="excerptLocation">
              </div>
              <div class="short-inputs">
                </div>
              </article>
              <article id="citation-form-right">
                <label for="publisher">Publisher</label>
              <input type="text" class="long-input" placeholder="Publisher" id="publisher-input" formControlName="publisher">
              <label for="publish-date">Publish Date <span class="required">*</span></label>
              <input type="date" id="publish-date" placeholder="Publish Date" class="long-input" formControlName="publishDate">
              <label for="source-type">Source Type <span class="required">*</span></label>
              <select name="source-type" id="source-type" formControlName="sourceType">
                <option value="">Source Type</option>
                <option *ngFor="let sourceType of sourceTypes" [ngValue]="sourceType">{{sourceType}}</option>
              </select>
              <label for="citation-format">Citation Format <span class="required">*</span></label>
              <select name="citation-format" id="citation-format" formControlName="format">
               <option value="">Citation Format</option>
               <option *ngFor="let format of formats" [ngValue]="format">{{format}}</option>
              </select>
            </article>
          </div>
          <div id="button-container">
            <button id="cancel-button" (click)="cancel()">Cancel</button>
            <button id="submit-button" (click)="submitExcerpt()">Save Excerpt</button>
          </div>
        </section>
    </form>
    </section>
  `,
  styleUrl: './new-excerpt.component.css'
})
export class NewExcerptComponent {
  private userDetailsService = inject(UserDetailsService);
  private localStorageService = inject(LocalStorageService);
  private excerptService = inject(ExcerptService);
  private router = inject(Router);

  userDetails: IUserProfile = JSON.parse(<string>this.localStorageService.get("userDetails"));

  excerptForm = new FormGroup({
    // top sections
    title: new FormControl('', Validators.compose([Validators.required, Validators.maxLength(200)])),
    excerpt: new FormControl('', Validators.compose([Validators.required, Validators.maxLength(800)])),
    // citation form
    excerptLocation: new FormControl('', Validators.compose([Validators.maxLength(30)])),
    format: new FormControl('', Validators.required),
    publisher: new FormControl('', Validators.compose([Validators.maxLength(80)])),
    publishDate: new FormControl(new Date()),
    authorFirstName: new FormControl('', Validators.compose([Validators.required, Validators.maxLength(100)])),
    authorLastName: new FormControl('', Validators.maxLength(100)),
    sourceType: new FormControl('', Validators.required),
    sourceTitle: new FormControl('', Validators.compose([Validators.required, Validators.maxLength(200)])),
    sourceEdition: new FormControl('', Validators.maxLength(30)),
  });

  // select control values
  formats: string[] = ['MLA', 'APA', 'Chicago'];
  sourceTypes: string[] = ['Book', 'Article', 'Journal', 'Other'];

  @Output() isCancelling = new EventEmitter<boolean>();

  cancel() {
    this.isCancelling.emit(true);
  }

  submitExcerpt() {
    let excerptCard: IExcerptCard = {
      excerptCardId: 0,
      title: this.excerptForm.value.title,
      excerpt: this.excerptForm.value.excerpt,
      creationDate: new Date(),
      lastUpdatedDate: new Date(),
      userProfileId: this.userDetails.userProfileId,
      citationId: 0,
      citation: {
        citationId: 0,
        format: this.excerptForm.value.format,
        excerptLocation: this.excerptForm.value.excerptLocation,
        creationDate: new Date(),
        userProfileId: this.userDetails.userProfileId,
        sourceMaterialId: 0,
        sourceMaterial: {
          sourceMaterialId: 0,
          title: this.excerptForm.value.sourceTitle,
          publishDate: this.excerptForm.value.publishDate,
          publisher: this.excerptForm.value.publisher,
          sourceMaterialType: this.excerptForm.value.sourceType,
          sourceMaterialEdition: this.excerptForm.value.sourceEdition,
          authorFirstName: this.excerptForm.value.authorFirstName,
          authorLastName: this.excerptForm.value.authorLastName,
          userProfileId: this.userDetails.userProfileId
        }
      },
      keywords: [],
    }

    console.log(excerptCard);
    
    if (this.excerptForm.valid) {
      const response = this.excerptService.postExcerptAsync(excerptCard).then(result => {
        if(result['excerptCardId'] > 0) {
          this.router.navigate([`excerpt/${result['excerptCardId']}`]);
        }
      });
    }
  }

}
