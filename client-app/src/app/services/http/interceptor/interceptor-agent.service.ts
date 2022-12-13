import {
  HttpEvent,
  HttpHandler,
  HttpHeaders,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable, isDevMode } from '@angular/core';
import { exhaustMap, Observable, take } from 'rxjs';
import { Endpoints, _Endpoints } from 'src/app/types/endpoints';
import { AuthService } from '../auth/auth.service';

/**
 * Intercept all HTTP calls, and bearer token to each request
 */
@Injectable({
  providedIn: 'root',
})
export class InterceptorAgentService implements HttpInterceptor {
  constructor(private _authService: AuthService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const BASE_API_ENDPOINT = isDevMode()
      ? 'https://localhost:5000/api/'
      : `${window.location.origin}/api/`;

    let apiReq: HttpRequest<any>;
    let endpoint = Endpoints.API;

    if (req.headers.has('useEndpoint')) {
      const reqEndpoint = req.headers.get('useEndpoint')!;
      // @ts-ignore
      endpoint = Endpoints[reqEndpoint] as string;
    }

    return this._authService.user$.pipe(
      take(1),
      exhaustMap((user) => {
        let headers = new HttpHeaders();

        if (user !== null) {
          headers = headers.append('Authorization', `Bearer ${user.token}`);
        }

        if (!req.headers.has('useForm')) {
          headers = headers.append('Content-Type', 'application/json');
        }

        switch (endpoint) {
          case Endpoints.API: {
            apiReq = req.clone({
              url: `${BASE_API_ENDPOINT}${req.url}`,
              headers,
            });

            break;
          }
        }

        return next.handle(apiReq);
      })
    );
  }
}
