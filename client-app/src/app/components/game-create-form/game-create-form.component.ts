import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { forkJoin, Observable, Subject, switchMap, takeUntil } from 'rxjs';
import {
  GameDTO,
  ICoverArt,
  IGameCreateDTO,
} from 'src/app/interfaces/games.interface';
import { CommonService } from 'src/app/services/common/common.service';
import { GameService } from 'src/app/services/http/games/game.service';

@Component({
  selector: 'app-game-create-form',
  templateUrl: './game-create-form.component.html',
  styleUrls: ['./game-create-form.component.scss'],
})
export class GameCreateFormComponent implements OnInit {
  private destroy$ = new Subject<void>();
  categories: { type: string; value: boolean }[] = [];

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
    private _commonService: CommonService
  ) {}

  ngOnInit(): void {
    // get all category values
    this._gameService
      .getAllCategoryTypes()
      .pipe(takeUntil(this.destroy$))
      .subscribe((types) => {
        types.forEach((_type) => {
          this.categories.push({
            type: _type,
            value: false,
          });
        });
      });
  }

  /**
   * Submit the games first as we need the game ids before adding images to them
   */
  handleFormSubmit() {
    const { title, description, category, price, link } =
      this.gamesEditForm.value;

    if (
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
      const gameDTO: IGameCreateDTO = {
        title,
        description,
        category,
        price,
        stock: 10,
        youtubeLink: link,
      };

      this._commonService.loader$.next(true);
      this._commonService.showSpinner$.next(true);
      this._commonService.message$.next('Loading...');

      this._gameService
        .createGame(gameDTO)
        .pipe(
          takeUntil(this.destroy$),
          switchMap((game) => {
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
              formData.append('Id', game.id);
              formData.append('IsBoxArt', `${isBoxArt}`);

              fileObservables.push(this._gameService.addImage(formData));
            });

            const source = forkJoin(fileObservables);

            return source;
          })
        )
        .subscribe({
          next: () => {
            this._commonService.showSpinner$.next(false);

            this._commonService.icon$.next(
              'fa-check splash-screen-icon--success'
            );
            this._commonService.message$.next('Game Created');
          },
          error: () => {
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

  /**
   * Change the values for the checkboxes that are looked at
   * @param event
   * @param category
   */
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
