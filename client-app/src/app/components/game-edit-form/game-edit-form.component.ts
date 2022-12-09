import { DOCUMENT } from '@angular/common';
import {
  AfterViewInit,
  Component,
  ElementRef,
  Inject,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { forkJoin, Observable, Subject, switchMap, takeUntil } from 'rxjs';
import {
  GameDTO,
  ICoverArt,
  IGame,
  IGameEditDTO,
} from 'src/app/interfaces/games.interface';
import { CommonService } from 'src/app/services/common/common.service';
import { GameService } from 'src/app/services/http/games/game.service';

@Component({
  selector: 'app-game-edit-form',
  templateUrl: './game-edit-form.component.html',
  styleUrls: ['./game-edit-form.component.scss'],
})
export class GameEditFormComponent implements OnInit, OnDestroy, AfterViewInit {
  private destroy$ = new Subject<void>();
  selectedGame: IGame | null = null;
  categories: { type: string; value: boolean }[] = [];
  index = 0;
  games: IGame[] = [];

  gamesEditForm = new FormGroup({
    title: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    category: new FormArray([]),
    price: new FormControl(0, [Validators.required]),
    link: new FormControl('', [Validators.required]),
  });

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private _gameService: GameService,
    private _commonService: CommonService,
    private _activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this._activatedRoute.params.subscribe((params) => {
      this.index = params['index'];
    });

    this._gameService.gamesList$
      .pipe(takeUntil(this.destroy$))
      .subscribe((games) => {
        this.games = games;
      });

    this._gameService
      .getAllCategoryTypes()
      .pipe(
        takeUntil(this.destroy$),
        switchMap((types) => {
          types.forEach((_type) => {
            this.categories.push({
              type: _type,
              value: false,
            });
          });

          return this._gameService.selectedGame$;
        })
      )
      .subscribe((game) => {
        this.selectedGame = game;

        this.gamesEditForm.controls.title.setValue(game.title);
        this.gamesEditForm.controls.description.setValue(game.description);
        this.gamesEditForm.controls.price.setValue(game.price);
        this.gamesEditForm.controls.link.setValue(game.youtubeLink);

        game.category.forEach((category) => {
          //@ts-ignore
          this.gamesEditForm.controls.category.push(new FormControl(category));

          const index = this.categories.findIndex((x) => x.type === category);

          this.categories[index!].value = true;
        });
      });

    this.onChanges();
  }

  ngAfterViewInit(): void {}

  onChanges() {
    this.gamesEditForm.valueChanges.subscribe((val) => {
      if (val.title?.length) {
        this.document
          .getElementById('label-title')
          ?.classList.add('form__label-active');
      } else {
        this.document
          .getElementById('label-title')
          ?.classList.remove('form__label-active');
      }
    });
  }

  handleFormSubmit() {
    const { title, description, category, price, link } =
      this.gamesEditForm.value;

    const coverArts = [];

    const largeImageInput = this.document.getElementById(
      'large-image'
    ) as HTMLInputElement;

    const smallImageInput = this.document.getElementById(
      'small-image'
    ) as HTMLInputElement;

    let largeImage;
    let smallImage;

    if (
      largeImageInput.files !== null &&
      largeImageInput.files.length &&
      largeImageInput.files
    ) {
      largeImage = largeImageInput.files[0];
      coverArts.push({ file: largeImage, isBoxArt: false });
    }

    if (
      smallImageInput.files !== null &&
      smallImageInput.files.length &&
      smallImageInput.files
    ) {
      smallImage = smallImageInput.files[0];
      coverArts.push({ file: smallImage, isBoxArt: true });
    }

    const fileObservables: Observable<ICoverArt>[] = [];
    let createdCoverArts: ICoverArt[] = [];

    coverArts.forEach(({ file, isBoxArt }) => {
      const formData = new FormData();

      formData.append('File', file);
      formData.append('Path', 'logo/cover-art');
      formData.append('Id', this.selectedGame!.id);
      formData.append('IsBoxArt', `${isBoxArt}`);

      fileObservables.push(this._gameService.addImage(formData));
    });

    if (fileObservables.length) {
      const source = forkJoin(fileObservables);

      source.subscribe((coverArt) => {
        createdCoverArts = coverArt;
      });
    }

    if (
      this.selectedGame !== null &&
      title !== undefined &&
      title !== null &&
      description !== undefined &&
      description !== null &&
      category !== undefined &&
      category !== null &&
      price !== undefined &&
      price !== null &&
      link !== undefined &&
      link !== null
    ) {
      const gameDTO: IGameEditDTO = {
        id: this.selectedGame.id,
        title,
        description,
        category,
        price,
        youtubeLink: link,
      };

      this._commonService.loader$.next(true);
      this._commonService.showSpinner$.next(true);
      this._commonService.message$.next('Loading...');

      this._gameService
        .editGame(this.selectedGame.id, gameDTO)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: (games: GameDTO) => {
            this._commonService.showSpinner$.next(false);

            this._commonService.icon$.next(
              'fa-check splash-screen-icon--success'
            );
            this._commonService.message$.next('Game updated');
          },
          error: (err) => {
            this._commonService.showSpinner$.next(false);
            this._commonService.icon$.next(
              'fa-xmark splash-screen-icon--error'
            );
            this._commonService.message$.next('Could not updated game!');

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

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.unsubscribe();
  }

  onCheckboxChange(event: any, category: any) {
    //@ts-ignore
    const selectedCategory = this.gamesEditForm.controls.category as FormArray;

    category.value = !category.value;

    if (event.target.checked) {
      selectedCategory.push(new FormControl(category.type));
    } else {
      const index = selectedCategory.controls.findIndex(
        (x) => x.value === category.type
      );
      selectedCategory.removeAt(index);
    }
  }

  handleLargeFileUpload() {
    this.document.getElementById('large-image')?.click();
  }

  handleSmallFileUpload() {
    this.document.getElementById('small-image')?.click();
  }
}
