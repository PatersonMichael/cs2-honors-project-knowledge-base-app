import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IExcerptCard } from '../../models/IExcerptCard';
import { ExcerptService } from '../../services/excerpt.service';
import { ActivatedRoute, Router } from '@angular/router';
import { UserNavComponent } from '../user-nav/user-nav.component';
import { ICitation } from '../../models/ICitation';
import { ISourceMaterial } from '../../models/ISourceMaterial';
import { ReactiveFormsModule, FormGroup, FormControl } from '@angular/forms';
import { UserDetailsService } from '../../services/user-details.service';
import { LocalStorageService } from '../../services/local-storage.service';
import { IUserProfile } from '../../models/IUserProfile';

// TODO fix input for publish date, format, and sourceMaterialType

@Component({
  selector: 'app-excerpt-details',
  standalone: true,
  imports: [CommonModule, UserNavComponent, ReactiveFormsModule],
  template:`
    <app-user-nav></app-user-nav>
    <section class="excerpt-details-page">
      <section id="excerpt">
        <div id="excerpt-heading">
          <h1 id="excerpt-title" [textContent]="excerptCard?.title" [contentEditable]="editMode" (input)="$any(excerptCard).title = $any($event).target.innerHTML"></h1>
          <img (click)="editMode = true" src="assets/edit-icon.png" alt="Edit">
        </div>
        <article id="excerpt-body" [contentEditable]="editMode" [textContent]="excerptBody" (input)="$any(excerptCard).excerpt = $any($event).target.innerHTML">
        </article>
        <article id="citation">
            <h2>Citation</h2>
        @if (editMode && excerptCard?.citation?.userProfileId == userDetails?.userProfileId) {
          <div id="citation-container">
            <article id="citation-form-left">
              <p class="long-input" id="source-title" [contentEditable]="editMode" 
              (input)="$any(excerptCard).citation.sourceMaterial.title = $any($event).target.innerHTML">
              {{excerptCard?.citation?.sourceMaterial?.title}}
              </p>
              <p class="long-input" id="source-edition" [contentEditable]="editMode" 
                (input)="$any(excerptCard).citation.sourceMaterial.sourceMaterialEdition = $any($event).target.innerHTML">
                {{excerptCard?.citation?.sourceMaterial?.sourceMaterialEdition}}
              </p>
                <p class="long-input" id="author-first-name" [contentEditable]="editMode" 
                  (input)="$any(excerptCard).citation.sourceMaterial.authorFirstName = $any($event).target.innerHTML">
                  {{excerptCard?.citation?.sourceMaterial?.authorFirstName}}
                </p>
                <p class="long-input" id="author-last-name" [contentEditable]="editMode" 
                  (input)="$any(excerptCard).citation.sourceMaterial.authorLastName = $any($event).target.innerHTML">
                  {{excerptCard?.citation?.sourceMaterial?.authorLastName}}
              </p>
              <div class="short-inputs">
                </div>
              </article>
              <article id="citation-form-right">
              <p class="long-input" id="excerpt-location" [contentEditable]="editMode" 
                (input)="$any(excerptCard).citation.excerptLocation = $any($event).target.innerHTML">
                {{excerptCard?.citation?.excerptLocation}}
              </p>
              <p class="long-input" id="publisher" [contentEditable]="editMode" 
                (input)="$any(excerptCard).citation.sourceMaterial.publisher = $any($event).target.innerHTML">
                {{excerptCard?.citation?.sourceMaterial?.publisher}}
              </p>
              <!-- <form [formGroup]="selectControlForm">
              <label for="publish-date">Publish Date</label>
              <input type="date" [value]="excerptCard?.citation?.sourceMaterial?.publishDate?.toString()?.slice(0, 10)" id="publish-date" placeholder="Publish Date" class="short-input" 
              (input)="updateDate()" formControlName="publishDate">
                <select name="source-type" id="source-type" formControlName="sourceMaterialType"
                (selection)="updateSourceType">
                  <option value="">Source Type</option>
                  <option *ngFor="let sourceType of sourceTypes" [ngValue]="sourceType">{{sourceType}}</option>
                </select>
                <select name="citation-format" id="citation-format" formControlName="format"
                >
                 <option value="">Citation Format</option>
                 <option *ngFor="let format of formats" (selectionChange)="updateFormat" [ngValue]="format">{{format}}</option>
                </select>
            </form> -->
            </article>
          </div>
        } 
        @else {
          
            @if(excerptCard?.citation?.format == "MLA") {
              <p class="citation-text">
                {{excerptCard?.citation?.sourceMaterial?.authorLastName}}, 
                {{excerptCard?.citation?.sourceMaterial?.authorFirstName}}.
                <span class="italics">{{excerptCard?.citation?.sourceMaterial?.title}}</span>.
                {{excerptCard?.citation?.sourceMaterial?.publisher}},
                {{excerptCard?.citation?.sourceMaterial?.publishDate?.toString()?.slice(0,4)}}.
              </p>
            }
            @else if (excerptCard?.citation?.format == "APA") {
              <p class="citation-text">
                {{excerptCard?.citation?.sourceMaterial?.authorLastName}}. 
                {{excerptCard?.citation?.sourceMaterial?.authorFirstName?.slice(0,1)}}. 
                ({{excerptCard?.citation?.sourceMaterial?.publishDate?.toString()?.slice(0,4)}}).
                <span class="italics">{{excerptCard?.citation?.sourceMaterial?.title}}</span>.
                {{excerptCard?.citation?.sourceMaterial?.publisher}},
  
  
              </p>
            }
            @else if (excerptCard?.citation?.format == "Chicago") {
              <p class="citation-text">
                {{excerptCard?.citation?.sourceMaterial?.authorFirstName}} 
                {{excerptCard?.citation?.sourceMaterial?.authorLastName}}.
                <span class="italics">{{excerptCard?.citation?.sourceMaterial?.title}}</span>.
                {{excerptCard?.citation?.sourceMaterial?.publisher}},
                {{excerptCard?.citation?.sourceMaterial?.publishDate?.toString()?.slice(0,4)}}, 
                {{excerptCard?.citation?.excerptLocation}}.
              </p>
            }
            
          }
        </article>
        <div id="footer">
          <button id="delete-button" [disabled]="!editMode"
           (click)="wantsToDelete = true"  [hidden]="!editMode">Delete</button>
          <button id="save-changes-button" [disabled]="!editMode"
           (click)="saveChanges()"  [hidden]="!editMode">Save Changes</button>
          @if (wantsToDelete) {
            <article id="check-delete">
              <p>Are you sure you want to delete this excerpt?</p>
              <div id="choice-buttons">
                <button id="yes-button" (click)="deleteNote()">Yes</button>
                <button id="no-button" (click)="wantsToDelete = false">No</button>

              </div>
            </article>
          }
        </div>

      </section>
    </section>
  `,
  styleUrl: './excerpt-details.component.css'
})
export class ExcerptDetailsComponent {
  route = inject(ActivatedRoute);
  router = inject(Router);
  excerptService = inject(ExcerptService);
  localStorageService = inject(LocalStorageService);

  userDetails: IUserProfile | null = JSON.parse(<string>this.localStorageService.get("userDetails"));

  excerptCard: IExcerptCard | undefined;
  citation: ICitation | undefined;
  sourceMaterial: ISourceMaterial | undefined;

  editMode: boolean = false;
  wantsToDelete: boolean = false;

  excerptBody: string = '';

  // select control values
  formats: string[] = ['MLA', 'APA', 'Chicago'];
  sourceTypes: string[] = ['Book', 'Article', 'Journal', 'Other'];

  selectControlForm = new FormGroup({
    publishDate: new FormControl(new Date()),
    format: new FormControl(''),
    sourceMaterialType: new FormControl(''),
  })
  
  constructor() {
    const noteCardId = parseInt(this.route.snapshot.params['id'], 10);
    this.excerptService.getExcerptAsync(noteCardId).then(result => {
      this.excerptCard = result;
      this.excerptBody = result['excerpt'];
    });

    this.citation = <ICitation>this.excerptCard?.citation;
    this.sourceMaterial= <ISourceMaterial>this.excerptCard?.citation?.sourceMaterial;
  }

  updateFormat() {
    console.log("update format");
    if (this.excerptCard?.citation?.format != null) {
      this.excerptCard.citation.format = this.selectControlForm.value.format;

    }
  }

  updateSourceType() {
    console.log("update source mat");
    if (this.excerptCard?.citation?.sourceMaterial.sourceMaterialType != null) {
      this.excerptCard.citation.sourceMaterial.sourceMaterialType = this.selectControlForm.value.sourceMaterialType;

    }
  }

  updateDate() {
    if (this.excerptCard?.citation?.sourceMaterial != null) {
      this.excerptCard.citation.sourceMaterial.publishDate = this.selectControlForm.value.publishDate;

    }
  }

  onChangeUpdate(dest: any, data: any) {
    dest = data;
  }

  saveChanges() {
    this.excerptCard!.lastUpdatedDate = new Date();
    this.excerptService.putExcerptAsync(<IExcerptCard>this.excerptCard).then(result => {
      console.log(result);
      
    })

    // console.log(this.excerptCard);
    
  }

  deleteNote() {
    this.excerptService.deleteExcerptAsync(<number>this.excerptCard!.excerptCardId);
    setTimeout(() => {this.router.navigate(['user/notes'])}, 1000);
  }

}
