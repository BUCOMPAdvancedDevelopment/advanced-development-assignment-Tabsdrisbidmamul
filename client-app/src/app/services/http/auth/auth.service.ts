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
  IPasswordChange,
  IUserDisplayNameAndImage,
} from 'src/app/interfaces/user.interface';
import { AgentService } from '../agent/agent.service';

/**
 * Auth class for authentication
 */
@Injectable({
  providedIn: 'root',
})
export class AuthService extends AgentService {
  private readonly AUTH_URL = 'account/login';
  private readonly SIGNUP_URL = 'account/signup';
  private readonly PASSWORD_CHANGE_URL = 'account/change-password';
  private readonly REFRESH_TOKEN_URL = 'account/refresh-token';
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

  /**
   * Get a new JWT token
   */
  refreshToken = () => {
    this.post<{}, IUserDTO>(this.REFRESH_TOKEN_URL, {})
      .pipe(catchError(this.handleAuthError), tap(this.handleAuth))
      .subscribe();
  };

  /**
   * Start auto refresh timer
   * @param user
   */
  private startRefreshTokenTimer(user: IUserDTO) {
    const jwtToken = JSON.parse(atob(user.token.split('.')[1]));

    const expires = new Date(jwtToken.exp * 1000);

    // 30 second before token expires
    const timeout = expires.getTime() - Date.now() - 30 * 1000;

    this.tokenTimerInterval = setTimeout(this.refreshToken, timeout);
  }

  private stopRefreshTokenTimer() {
    clearTimeout(this.tokenTimerInterval);
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
      this.stopRefreshTokenTimer();
    }

    this.tokenTimerInterval = null;
  }

  signup(signupDto: ISignupDTO) {
    return this.post<ISignupDTO, IUserDTO>(this.SIGNUP_URL, signupDto).pipe(
      catchError(this.handleAuthError),
      tap(this.handleAuth)
    );
  }

  changePassword(passwordChange: IPasswordChange) {
    return this.post<IPasswordChange, IUserDTO>(
      this.PASSWORD_CHANGE_URL,
      passwordChange
    ).pipe(catchError(this.handleAuthError), tap(this.handleAuth));
  }

  getUserFromUsername(username: string) {
    return this.get<IUserDisplayNameAndImage>(`account/${username}`);
  }

  /**
   * Get the user from the caller, and store the user in the observable
   * @param res
   * @returns
   */
  private handleAuth = (res: string | IUserDTO) => {
    if (res instanceof String || res === null) {
      console.error('ERROR: user is not authorised ', res);
      return;
    }

    if (this.tokenTimerInterval) {
      this.stopRefreshTokenTimer();
    }

    const _res = res as IUserDTO;

    this.storeUser(_res);
    this.startRefreshTokenTimer(_res);
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

  /**
   * Get error from caller, and pass the error message down the pipeline
   * @param err
   * @returns
   */
  private handleAuthError(err: HttpErrorResponse): Observable<string> {
    let errorMsg = err.error;

    console.log('err.headers ', err.headers);

    if (
      err.status == 401 &&
      err.headers.has('www-Authenticate') &&
      err.headers
        .get('www-Authenticate')
        ?.startsWith('Bearer error="invalid_token"')
    ) {
      console.log("Token has expired, we're logging the user out");
      this.logout();
    }

    return throwError(() => errorMsg);
  }
}
