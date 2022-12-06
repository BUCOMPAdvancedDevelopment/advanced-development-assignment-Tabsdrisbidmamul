import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Subject, switchMap, takeUntil } from 'rxjs';
import { IGame } from 'src/app/interfaces/games.interface';
import { IReviews } from 'src/app/interfaces/review.interface';
import { GameService } from 'src/app/services/http/games/game.service';
import { ReviewService } from 'src/app/services/http/reviews/review.service';

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
  reviews!: IReviews;

  constructor(
    private _route: ActivatedRoute,
    private _gameService: GameService,
    private _reviewService: ReviewService
  ) {}

  ngOnInit(): void {
    const id = this._route.snapshot.params['id'];

    this._gameService
      .getGame(id)
      .pipe(
        takeUntil(this.destroy$),
        switchMap((game) => {
          this.game = { ...game, createdAt: new Date(game.createdAt) };

          game.coverArt.forEach((image) => {
            if (!image.isBoxArt) {
              this.image = image.url;
            }
          });

          this.youtubeLink = `https://www.youtube.com/embed/${game.youtubeLink}`;

          return this._reviewService.getReviews(game.id);
        })
      )
      .subscribe((reviews) => {
        console.log('reviews ', reviews);
        this.reviews = reviews;
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.unsubscribe();
  }
}
