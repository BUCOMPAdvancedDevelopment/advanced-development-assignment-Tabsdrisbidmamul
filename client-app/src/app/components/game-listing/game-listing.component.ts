import { DOCUMENT } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import {
  AfterViewInit,
  Component,
  ElementRef,
  Inject,
  OnDestroy,
  OnInit,
  QueryList,
  ViewChild,
  ViewChildren,
} from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import {
  catchError,
  forkJoin,
  from,
  map,
  observable,
  of,
  Subject,
  switchMap,
  takeUntil,
  throwError,
} from 'rxjs';
import { IGame } from 'src/app/interfaces/games.interface';
import { IReviews, ReviewDTO } from 'src/app/interfaces/review.interface';
import {
  IUserDisplayNameAndImage,
  IUserDisplayNameAndImageAndReviewsRating,
  IUserDTO,
} from 'src/app/interfaces/user.interface';
import { BasketService } from 'src/app/services/common/basket.service';
import { CommonService } from 'src/app/services/common/common.service';
import { AuthService } from 'src/app/services/http/auth/auth.service';
import { GameService } from 'src/app/services/http/games/game.service';
import { ReviewService } from 'src/app/services/http/reviews/review.service';

@Component({
  selector: 'app-game-listing',
  templateUrl: './game-listing.component.html',
  styleUrls: ['./game-listing.component.scss'],
})
export class GameListingComponent implements OnInit, OnDestroy, AfterViewInit {
  private destroy$ = new Subject<void>();
  game!: IGame;
  image: string = '';
  youtubeLink = '';
  userDisplayNameAndImageAndReviews: IUserDisplayNameAndImageAndReviewsRating[] =
    [];
  errorMessage = '';
  user: IUserDTO | null = null;
  @ViewChildren('stars') private stars!: QueryList<ElementRef>;
  @ViewChild('review') private reviewDescription!: ElementRef;

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private _route: ActivatedRoute,
    private _gameService: GameService,
    private _reviewService: ReviewService,
    private _authService: AuthService,
    private _commonService: CommonService,
    private _basketService: BasketService
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
        }),
        catchError((error: HttpErrorResponse) => {
          if (error.status === 204) {
            this.userDisplayNameAndImageAndReviews = [];
            this.errorMessage = 'No reviews';
            return of(null);
          }

          return throwError(() => 'No reviews');
        })
      )
      .subscribe(this.storeReviews);

    this._authService.user$.pipe(takeUntil(this.destroy$)).subscribe((user) => {
      this.user = user;
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.unsubscribe();
  }

  ngAfterViewInit(): void {
    this.stars.changes.subscribe(() => {
      this.stars.toArray().forEach((element: ElementRef) => {
        (element.nativeElement as HTMLElement).addEventListener(
          'click',
          (e) => {
            (e.target as HTMLElement).classList.toggle('active');
          }
        );
      });
    });
  }

  storeReviews = (reviews: IReviews | null) => {
    if (reviews === null) return;

    const usernameObservable = reviews.reviews.map((reviews) =>
      this._authService.getUserFromUsername(reviews.username)
    );

    const source = forkJoin(usernameObservable);

    source.subscribe({
      next: (users) => {
        users.forEach((user, i) => {
          this.userDisplayNameAndImageAndReviews.push({
            displayName: user.displayName,
            image: user.image,
            review: reviews.reviews[i].review,
            rating: +reviews.reviews[i].rating,
          });
        });
      },
    });
  };

  getUserImageAndDisplayName(username: string) {
    return this._authService.getUserFromUsername(username);
  }

  createIterable(counter: number) {
    return new Array(counter);
  }

  handleSubmit() {
    const review = (this.reviewDescription.nativeElement as HTMLTextAreaElement)
      .value;

    const ratings = this.document.querySelectorAll('.active');

    const reviewDTO = new ReviewDTO(
      this.game.id,
      review,
      ratings.length.toString()
    );

    this._commonService.loader$.next(true);
    this._commonService.showSpinner$.next(true);

    this._reviewService
      .addReview(reviewDTO)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: () => {
          this._reviewService
            .getReviews(this.game.id)
            .pipe(takeUntil(this.destroy$))
            .subscribe((reviews) => {
              this._commonService.showSpinner$.next(false);
              this._commonService.icon$.next(
                'fa-check splash-screen-icon--success'
              );
              this._commonService.message$.next('Review added!');
              this.errorMessage = '';
              this.storeReviews(reviews);

              (
                this.reviewDescription.nativeElement as HTMLTextAreaElement
              ).value = '';

              this.document.querySelectorAll('.active').forEach((element) => {
                element.classList.remove('active');
              });
            });
        },
        error: () => {
          this._commonService.icon$.next('fa-xmark splash-screen-icon--error');
          this._commonService.message$.next('Could not add review!');
          this._commonService.showSpinner$.next(false);

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

  addToCart() {
    this._basketService.addItem(this.game);
  }
}
