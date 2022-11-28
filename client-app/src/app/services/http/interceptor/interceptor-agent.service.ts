import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable, isDevMode } from '@angular/core';
import { exhaustMap, Observable, take } from 'rxjs';
import { Endpoints, _Endpoints } from 'src/app/types/endpoints';
import { AuthService } from '../auth/auth.service';

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

    switch (endpoint) {
      case Endpoints.API: {
        apiReq = req.clone({
          url: `${BASE_API_ENDPOINT}${req.url}`,
          headers: req.headers.delete('useEndpoint', 'API'),
        });
        break;
      }
    }

    // @ts-ignore
    return next.handle(apiReq);

    // return this._authService.user$.pipe(
    //   take(1),
    //   exhaustMap((user) => {
    //     switch (endpoint) {
    //       case Endpoints.API: {
    //         apiReq = req.clone({
    //           url: `${BASE_API_ENDPOINT}${req.url}`,
    //           headers: req.headers.delete('useEndpoint', 'API'),
    //         });
    //         break;
    //       }
    //     }

    //     return next.handle(apiReq);
    //   })
    // );
  }
}
