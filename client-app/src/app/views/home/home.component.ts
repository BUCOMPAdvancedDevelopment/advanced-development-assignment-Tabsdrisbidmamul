import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { IGame } from 'src/app/interfaces/games.interface';
import { GameService } from 'src/app/services/http/games/game.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();

  constructor(private _gameService: GameService) {}

  /**
   * Get all games and subscribe to changes
   */
  ngOnInit(): void {
    this._gameService
      .getAllGames()
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (games) => {
          const _games = games.map<IGame>((game) => {
            return {
              ...game,
              createdAt: new Date(game.createdAt),
            };
          });

          this._gameService.gamesList$.next(_games);
        },
      });
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
