import {
  Component,
  OnChanges,
  OnDestroy,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject, switchMap, takeUntil } from 'rxjs';
import { IGame } from 'src/app/interfaces/games.interface';
import { SearchService } from 'src/app/services/http/search/search.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
})
export class SearchComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  query = '';
  games: IGame[] = [];
  images: string[] = [];

  constructor(
    private _activateRoute: ActivatedRoute,
    private _searchService: SearchService
  ) {}

  /**
   * Debounce search, and get back results
   */
  ngOnInit(): void {
    const query = this._activateRoute.snapshot.params['query'];

    if (query !== '') {
      this._activateRoute.params
        .pipe(
          takeUntil(this.destroy$),
          switchMap((params) => {
            this.query = params['query'];

            return this._searchService.searchGames(this.query);
          })
        )
        .subscribe((games) => {
          const _games = games.map<IGame>((game) => {
            return {
              ...game,
              createdAt: new Date(game.createdAt),
            };
          });

          this.games = _games;

          const temp: string[] = [];

          _games.forEach((game) => {
            game.coverArt.forEach((image) => {
              if (image.isBoxArt) {
                temp.push(image.url);
              }
            });
          });

          this.images = temp;
        });
    }
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.unsubscribe();
  }
}
