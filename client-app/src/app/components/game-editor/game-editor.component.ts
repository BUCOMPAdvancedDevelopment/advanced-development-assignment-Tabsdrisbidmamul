import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { IGame } from 'src/app/interfaces/games.interface';
import { GameService } from 'src/app/services/http/games/game.service';

@Component({
  selector: 'app-game-editor',
  templateUrl: './game-editor.component.html',
  styleUrls: ['./game-editor.component.scss'],
})
export class GameEditorComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  games: IGame[] = [];

  constructor(private _gameService: GameService) {}

  ngOnInit(): void {
    this._gameService.gamesList$
      .pipe(takeUntil(this.destroy$))
      .subscribe((games) => {
        this.games = games;
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.unsubscribe();
  }
}
