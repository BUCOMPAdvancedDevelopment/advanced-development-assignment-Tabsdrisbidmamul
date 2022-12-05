import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { GameDTO, IGame } from 'src/app/interfaces/games.interface';
import { GameService } from 'src/app/services/http/games/game.service';

@Component({
  selector: 'app-catalogue',
  templateUrl: './catalogue.component.html',
  styleUrls: ['./catalogue.component.scss'],
})
export class CatalogueComponent implements OnInit, OnDestroy {
  destroy$ = new Subject<void>();
  games: IGame[] = [];
  images: string[] = [];

  constructor(
    private _route: ActivatedRoute,
    private _gameService: GameService
  ) {}

  ngOnInit(): void {
    this._route.data.subscribe((games) => {
      const gamesDTO = games[0] as GameDTO[];

      const _games = gamesDTO.map<IGame>((game) => {
        return {
          ...game,
          createdAt: new Date(game.createdAt),
        };
      });

      this._gameService.gamesList$.next(_games);

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

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
