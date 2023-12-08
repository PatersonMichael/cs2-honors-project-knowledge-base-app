import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { SignupComponent } from './components/signup/signup.component';
import { LoginComponent } from './components/login/login.component';
import { AboutComponent } from './components/about/about.component';
import { UserHomeComponent } from './components/user-home/user-home.component';
import { authGuard } from './services/authorize-guard.service';
import { NotesComponent } from './components/notes/notes.component';
import { ExcerptsComponent } from './components/excerpts/excerpts.component';
import { NoteDetailsComponent } from './components/note-details/note-details.component';
import { CreateComponent } from './components/create/create.component';
import { NewNoteComponent } from './components/new-note/new-note.component';
import { NewExcerptComponent } from './components/new-excerpt/new-excerpt.component';

export const routes: Routes = [
    {
        path: '',
        component: HomeComponent,
        title: 'Knowledge Base - Home'
    },
    {
        path: 'signup',
        component: SignupComponent,
        title: 'Knowledge Base - Sign up'
    },
    {
        path: 'login',
        component: LoginComponent,
        title: 'Knowledge Base - Log In'
    },
    {
        path: 'about',
        component: AboutComponent,
        title: 'Knowledge Base - About'
    },
    {
        path: 'user',
        component: UserHomeComponent,
        title: 'Knowledge Base - User Home',
        canActivate: [authGuard],
        children: [
            {
                path:'notes',
                component: NotesComponent,
                title: 'Knowledge Base - Notes',
                canActivate: [authGuard],
            },
            {
                path: 'excerpts',
                component: ExcerptsComponent,
                title: 'Knowledge Base - Excerpts',
                canActivate: [authGuard]
            },
            {
                path: 'create',
                component: CreateComponent,
                title: 'Knowledge Base - Create',
                canActivate: [authGuard],
                children: [
                    {
                        path: 'note',
                        component: NewNoteComponent,
                        title: 'Knowledge Base - New Note',
                        canActivate: [authGuard]
                    },
                    {
                        path: 'excerpt',
                        component: NewExcerptComponent,
                        title: 'Knowledge Base - New Excerpt',
                        canActivate: [authGuard]
                    },
                ]
            }

        ],
    },
    {
        path:'note/:id',
        component: NoteDetailsComponent,
        title: 'Knowledge Base - Note Details',
        canActivate: [authGuard]
    }
];

export default routes;