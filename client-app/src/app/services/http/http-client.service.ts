import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class HttpClientService {
  constructor(private _httpClient: HttpClient) {}

  getAllGames() {
    return this._httpClient.get('https://localhost:5000/api/games').pipe(
      catchError((e) => {
        console.log(e);
        return e;
      })
    );
  }
}
