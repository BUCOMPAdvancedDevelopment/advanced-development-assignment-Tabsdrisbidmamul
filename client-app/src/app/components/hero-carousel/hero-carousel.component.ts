import { DOCUMENT } from '@angular/common';
import { Component, Inject, Input, OnInit } from '@angular/core';
import { CloudinaryService } from 'src/app/services/cloudinary/cloudinary.service';

import Glide from '@glidejs/glide';
import { IGame } from 'src/app/interfaces/games.interface';
import { GameService } from 'src/app/services/http/games/game.service';

@Component({
  selector: 'app-hero-carousel',
  templateUrl: './hero-carousel.component.html',
  styleUrls: ['./hero-carousel.component.scss'],
})
export class HeroCarouselComponent implements OnInit {
  images: string[] = [];
  glide!: Glide.Properties;

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private _cloudinary: CloudinaryService,
    private _gameService: GameService
  ) {}

  ngOnInit(): void {
    this._gameService.gamesList$.subscribe((games) => {
      this.images = this._cloudinary.transformIdsToUrls(games);
    });
  }

  initGlide() {
    if (this.images) {
      this.glide = new Glide('.glide', {
        type: 'carousel',
        perView: 3,
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
