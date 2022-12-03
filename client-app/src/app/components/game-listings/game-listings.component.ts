import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { IGame } from 'src/app/interfaces/games.interface';
import { CloudinaryService } from 'src/app/services/cloudinary/cloudinary.service';
import { GameService } from 'src/app/services/http/games/game.service';

@Component({
  selector: 'app-game-listings',
  templateUrl: './game-listings.component.html',
  styleUrls: ['./game-listings.component.scss'],
})
export class GameListingsComponent implements OnInit, OnDestroy {
  destroy$ = new Subject<void>();
  games: IGame[] = [];

  constructor(
    private _gameService: GameService,
    private _cloudinaryService: CloudinaryService
  ) {}

  ngOnInit(): void {
    this._gameService.gamesList$
      .pipe(takeUntil(this.destroy$))
      .subscribe((games) => {
        this.games = games.map((game) => {
          return {
            ...game,
            url: this._cloudinaryService.transformIdToUrl(game, 'desktop'),
          };
        });
      });
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
