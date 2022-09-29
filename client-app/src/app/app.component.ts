import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { HttpClientService } from './services/http/http-client.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'client-app';
  private destroy$ = new Subject<void>();

  constructor(private _httpService: HttpClientService) {}

  ngOnInit(): void {
    this._httpService
      .getAllGames()
      .pipe(takeUntil(this.destroy$))
      .subscribe((games) => {
        console.log(games);
      });
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.unsubscribe();
  }
}
