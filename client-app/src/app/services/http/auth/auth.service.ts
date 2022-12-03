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
import {
  ILoginDTO,
  ISignupDTO,
  IUserDTO,
  SignupDTO,
} from 'src/app/interfaces/user.interface';
import { AgentService } from '../agent/agent.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService extends AgentService {
  private readonly AUTH_URL = 'account/login';
  private readonly SIGNUP_URL = 'account/signup';
  private tokenTimerInterval: any;

  user$ = new BehaviorSubject<IUserDTO | null>(null);

  constructor(protected override _http: HttpClient, private _router: Router) {
    super(_http);
  }

  autoLogin() {
    const user = this.getUserFromLocalStorage();
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

  logout() {
    this.user$.next(null);
    this.clearUserFromLocalStorage();
    this._router.navigate(['']);

    if (this.tokenTimerInterval) {
      clearInterval(this.tokenTimerInterval);
    }

    this.tokenTimerInterval = null;
  }

  signup(signupDto: ISignupDTO) {
    return this.post<ISignupDTO, IUserDTO>(this.SIGNUP_URL, signupDto).pipe(
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

    if (this.getUserFromLocalStorage() === null) {
      this.storeUserIntoLocalStorage(user);
    }
  }

  storeUserIntoLocalStorage(user: IUserDTO) {
    localStorage.setItem('userData', JSON.stringify(user));
  }

  private getUserFromLocalStorage(): IUserDTO | null {
    return JSON.parse(localStorage.getItem('userData')!);
  }

  clearUserFromLocalStorage() {
    if (this.getUserFromLocalStorage() !== null) {
      localStorage.removeItem('userData');
    }
  }

  private handleAuthError(err: HttpErrorResponse): Observable<string> {
    let errorMsg = err.error;

    return throwError(() => errorMsg);
  }
}
