import { HttpClient } from '@angular/common/http';
import { Injectable, isDevMode } from '@angular/core';
import { catchError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HttpClientService {
  private domain = isDevMode()
    ? 'https://localhost:5000/'
    : window.location.origin;

  constructor(private _httpClient: HttpClient) {}

  getAllGames() {
    console.log('isDevMode ', isDevMode());

    return this._httpClient.get(`${this.domain}/api/games`).pipe(
      catchError((e) => {
        console.log(e);
        return e;
      })
    );
  }
}
