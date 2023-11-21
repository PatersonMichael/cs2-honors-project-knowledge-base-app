import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { SignupComponent } from './signup/signup.component';
import { LoginComponent } from './login/login.component';
import { AboutComponent } from './about/about.component';

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
    }
];
