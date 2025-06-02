import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorMessageHttpInterceptor implements HttpInterceptor {
  
  constructor(private toastr: ToastrService) {
    
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    console.log('Interceptando requisição:', req.url);

    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        if(error.error && error.error.message){
          var message = error.error.message.replace('\n', "<br>") + '<br>';
          this.toastr.error(message, "Erro de Validação",  {
            enableHtml: true
          });
        }
        return throwError(() => error);
      })
    );
  }
}
