import { inject } from '@angular/core';
import { CanActivateFn, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AppCookieService } from './app-cookie.service';

export const authGuard: CanActivateFn = (
  next: ActivatedRouteSnapshot,
  state: RouterStateSnapshot ) => {
    if (inject(AppCookieService).get("Authorization")) {
      return true;
    }
    else {
      return inject(Router).createUrlTree(['/login']);
    }
  }