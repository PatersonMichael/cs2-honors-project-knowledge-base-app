import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { SignupComponent } from './components/signup/signup.component';
import { LoginComponent } from './components/login/login.component';
import { AboutComponent } from './components/about/about.component';
import { UserHomeComponent } from './components/user-home/user-home.component';
import { authGuard } from './services/authorize-guard.service';
import { NotesComponent } from './components/notes/notes.component';
import { ExcerptsComponent } from './components/excerpts/excerpts.component';

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
                canActivate: [authGuard]
            },
            {
                path: 'excerpts',
                component: ExcerptsComponent,
                title: 'Knowledge Base - Excerpts',
                canActivate: [authGuard]
            }

        ],
    }
];

export default routes;