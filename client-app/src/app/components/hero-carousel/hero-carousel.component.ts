import { DOCUMENT } from '@angular/common';
import {
  AfterViewInit,
  Component,
  HostListener,
  Inject,
  Input,
  OnInit,
} from '@angular/core';
import { CloudinaryService } from 'src/app/services/cloudinary/cloudinary.service';

import Glide from '@glidejs/glide';
import { IGame } from 'src/app/interfaces/games.interface';
import { GameService } from 'src/app/services/http/games/game.service';
import { BreakpointObserver, BreakpointState } from '@angular/cdk/layout';
import { Subject, takeUntil } from 'rxjs';
import { DeviceType } from 'src/app/types/deviceType';
import { debounce } from 'src/app/helpers/extensions';
import { Router } from '@angular/router';

@Component({
  selector: 'app-hero-carousel',
  templateUrl: './hero-carousel.component.html',
  styleUrls: ['./hero-carousel.component.scss'],
})
export class HeroCarouselComponent implements OnInit {
  private destory$ = new Subject<void>();
  games: IGame[] = [];
  images: string[] = [];
  glide!: Glide.Properties;

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private _cloudinary: CloudinaryService,
    private _gameService: GameService,
    private _router: Router
  ) {
    // this.onResize = debounce(this.onResize, 150, false);
  }

  /**
   * Get all games and set ip GlideJS
   */
  ngOnInit(): void {
    this._gameService.gamesList$.subscribe((games) => {
      let deviceType: DeviceType = 'desktop';

      if (window.innerWidth < 768) deviceType = 'mobile';

      this.games = games;

      const temp: string[] = [];

      games.forEach((game) => {
        game.coverArt.forEach((image) => {
          if (!image.isBoxArt) {
            temp.push(image.url);
          }
        });
      });

      this.images = temp;
    });
  }

  navigateToGame(index: number) {
    this._router.navigate(['catalogue', 'game', this.games[index].id]);
  }

  initGlide() {
    if (this.images.length === this.games.length) {
      this.glide = new Glide('.hero-carousel', {
        type: 'slider',
        perView: 1,
        focusAt: 'center',
        // bound: true,
        autoplay: 8000,
        hoverpause: true,
        gap: 30,
        peek: {
          before: 50,
          after: 50,
        },
        breakpoints: {
          992: {
            perView: 1,
            gap: 20,
            peek: 0,
          },
          576: {
            perView: 1,
            gap: 10,
            peek: 0,
          },
        },
      }).mount();
    }
  }
}
