import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';

@Injectable()
export class AppInterceptor implements HttpInterceptor {

    /**
     * Default endpoint, when there is no endpoint specified on the request
     */
    private defaultApiEndpoint: string = environment.apiEndpoint;

    constructor() {
    }

    intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
        request = this.checkApiEndpoint(request);

        ///refreshtoken
        ///https://indepth.dev/top-10-ways-to-use-interceptors-in-angular/
        return next.handle(request).pipe(
            catchError(error => {

                if ([401, 403].includes(error.status) /*&& this.appService.isLoggedIn()*/) {

                    if (error.status === 401) {
                        // this.appService.logout(undefined);
                    }
                    else if (error.status === 403) {
                        // this.appService.forbidden();
                    }
                }

                return throwError(error);
            })
        );

    }

    private checkApiEndpoint(request: HttpRequest<any>): HttpRequest<any> {
        if (request.url.indexOf("http://") === -1 && request.url.indexOf("https://") === -1) {
            return request.clone({
                url: this.defaultApiEndpoint + request.url
            });
        }

        return request;
    }
}