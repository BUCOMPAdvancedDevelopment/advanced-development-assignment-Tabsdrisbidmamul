import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { CloudinaryService } from 'src/app/services/cloudinary/cloudinary.service';

import Glide from '@glidejs/glide';

@Component({
  selector: 'app-hero-carousel',
  templateUrl: './hero-carousel.component.html',
  styleUrls: ['./hero-carousel.component.scss'],
})
export class HeroCarouselComponent implements OnInit {
  images: string[] = [];

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private _cloudinary: CloudinaryService
  ) {}

  ngOnInit(): void {
    const url = this._cloudinary.getImageUrl();
    console.log('url ', url);

    this.images = [944, 1011, 984, 230, 767, 80].map(
      (n) => `https://picsum.photos/id/${n}/900/500`
    );
  }

  initGlide() {
    if (this.images.length === 6) {
      new Glide('.glide', {
        type: 'slider',
        perView: 3,
        bound: true,
        autoplay: 8000,
        hoverpause: true,
        peek: {
          before: 50,
          after: 50,
        },
        breakpoints: {
          768: {
            perView: 2,
          },
          576: {
            perView: 1,
          },
        },
      }).mount();
    }
  }
}
