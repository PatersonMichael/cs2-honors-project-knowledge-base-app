import { HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { AppCookieService } from './app-cookie.service';
@Injectable({
  providedIn: 'root'
})
export class UniversalInterceptorService implements HttpInterceptor {
  private cookieService = inject(AppCookieService);

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    const token = this.cookieService.get("Authorization");
    req = req.clone({
      url: req.url,
      setHeaders: {
        Authorization: token,
      }
    });

    console.log(req);
    
    return next.handle(req);
  }

  constructor() { }
}
