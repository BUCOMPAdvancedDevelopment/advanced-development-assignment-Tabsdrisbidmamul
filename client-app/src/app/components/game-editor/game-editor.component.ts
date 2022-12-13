import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { GameDTO, IGame } from 'src/app/interfaces/games.interface';
import { CommonService } from 'src/app/services/common/common.service';
import { GameService } from 'src/app/services/http/games/game.service';

@Component({
  selector: 'app-game-editor',
  templateUrl: './game-editor.component.html',
  styleUrls: ['./game-editor.component.scss'],
})
export class GameEditorComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  games: IGame[] = [];

  constructor(
    private _gameService: GameService,
    private _router: Router,
    private _activateRoute: ActivatedRoute,
    private _commonService: CommonService
  ) {}

  ngOnInit(): void {
    this._gameService
      .getAllGames()
      .pipe(takeUntil(this.destroy$))
      .subscribe((games) => this.setGame(games));
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.unsubscribe();
  }

  navigateToGameEditForm(index: number) {
    this._gameService.selectedGame$.next(this.games[index]);
    this._router.navigate(['edit', index, this.games[index].id], {
      relativeTo: this._activateRoute,
    });
  }

  navigateToGameCreateForm() {
    this._router.navigate(['create'], { relativeTo: this._activateRoute });
  }

  setGame(games: GameDTO[]) {
    const _games = games.map<IGame>((game) => {
      return {
        ...game,
        createdAt: new Date(game.createdAt),
      };
    });

    this._gameService.gamesList$.next(_games);

    this.games = _games;
  }

  /**
   * Delete the game, remove the game from the observable, but also send a request to the API
   * @param index
   */
  removeGame(index: number) {
    this._commonService.loader$.next(true);
    this._commonService.showSpinner$.next(true);
    this._commonService.message$.next('Loading...');

    this._gameService
      .removeGame(this.games[index].id)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: () => {
          this._commonService.showSpinner$.next(false);

          this._commonService.icon$.next(
            'fa-check splash-screen-icon--success'
          );
          this._commonService.message$.next('Game removed');

          this.games.splice(index, 1);
          this._gameService.gamesList$.next(this.games);
        },
        error: () => {
          this._commonService.showSpinner$.next(false);
          this._commonService.icon$.next('fa-xmark splash-screen-icon--error');
          this._commonService.message$.next('Could not remove game!');

          setTimeout(() => {
            this._commonService.loader$.next(false);
            this._commonService.showSpinner$.next(true);
          }, 2500);
        },
        complete: () => {
          setTimeout(() => {
            this._commonService.loader$.next(false);
            this._commonService.showSpinner$.next(true);
          }, 2500);
        },
      });
  }
}
