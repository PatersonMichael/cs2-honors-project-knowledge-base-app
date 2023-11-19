import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { SignupComponent } from './signup/signup.component';

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
];
