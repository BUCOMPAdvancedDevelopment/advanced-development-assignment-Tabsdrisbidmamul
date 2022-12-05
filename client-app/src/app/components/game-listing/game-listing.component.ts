import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { IGame } from 'src/app/interfaces/games.interface';
import { GameService } from 'src/app/services/http/games/game.service';

@Component({
  selector: 'app-game-listing',
  templateUrl: './game-listing.component.html',
  styleUrls: ['./game-listing.component.scss'],
})
export class GameListingComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  game!: IGame;
  image: string = '';
  youtubeLink = '';

  constructor(
    private _route: ActivatedRoute,
    private _gameService: GameService
  ) {}

  ngOnInit(): void {
    const id = this._route.snapshot.params['id'];

    this._gameService
      .getGame(id)
      .pipe(takeUntil(this.destroy$))
      .subscribe((game) => {
        this.game = { ...game, createdAt: new Date(game.createdAt) };

        game.coverArt.forEach((image) => {
          if (!image.isBoxArt) {
            this.image = image.url;
          }
        });

        this.youtubeLink = `https://www.youtube.com/embed/${game.youtubeLink}`;
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.unsubscribe();
  }
}
