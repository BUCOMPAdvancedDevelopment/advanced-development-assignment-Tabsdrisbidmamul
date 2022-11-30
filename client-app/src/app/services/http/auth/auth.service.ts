import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {
  BehaviorSubject,
  catchError,
  Observable,
  ReplaySubject,
  tap,
  throwError,
} from 'rxjs';
import { ILoginDTO, IUserDTO } from 'src/app/interfaces/user.interface';
import { AgentService } from '../agent/agent.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService extends AgentService {
  private readonly AUTH_URL = 'account/login';
  private tokenTimerInterval = 0;

  user$ = new BehaviorSubject<IUserDTO | null>(null);

  constructor(protected override _http: HttpClient, private _router: Router) {
    super(_http);
  }

  autoLogin() {
    const user = this.getUserFromLocalStroage();
    if (user !== null) {
      const _user = user as IUserDTO;
      this.storeUser(_user);
    }
  }

  login(loginDto: ILoginDTO) {
    return this.post<ILoginDTO, IUserDTO>(this.AUTH_URL, loginDto).pipe(
      catchError(this.handleAuthError),
      tap(this.handleAuth)
    );
  }

  private handleAuth = (res: string | IUserDTO) => {
    if (res instanceof String) return;

    const _res = res as IUserDTO;

    this.storeUser(_res);
  };

  private storeUser(user: IUserDTO) {
    this.user$.next(user);

    if (this.getUserFromLocalStroage() === null) {
      this.storeUserIntoLocalStorage(user);
    }
  }

  private storeUserIntoLocalStorage(user: IUserDTO) {
    localStorage.setItem('userData', JSON.stringify(user));
  }

  private getUserFromLocalStroage(): IUserDTO | null {
    return JSON.parse(localStorage.getItem('userData')!);
  }

  private handleAuthError(err: HttpErrorResponse): Observable<string> {
    let errorMesg = err.message;

    return throwError(() => new Error(errorMesg));
  }
}
