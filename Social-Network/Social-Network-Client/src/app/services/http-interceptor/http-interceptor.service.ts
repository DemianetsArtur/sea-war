import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OptionsInfoService } from '../options-info/options-info.service';

@Injectable({
  providedIn: 'root'
})
export class HttpInterceptorService implements HttpInterceptor {
  constructor(private optionsInfo: OptionsInfoService) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = localStorage.getItem(this.optionsInfo.authToken);
    if (token) {
      req = req.clone({    
        setHeaders: {    
          Authorization: `Bearer ${token}`,    
        }    
      });   
    }
    return next.handle(req);
  }
}
