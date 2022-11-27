import { Component, OnInit } from '@angular/core';
import { CloudinaryService } from 'src/app/services/cloudinary/cloudinary.service';

@Component({
  selector: 'app-hero-carousel',
  templateUrl: './hero-carousel.component.html',
  styleUrls: ['./hero-carousel.component.scss'],
})
export class HeroCarouselComponent implements OnInit {
  images = [944, 1011, 984].map((n) => `https://picsum.photos/id/${n}/900/500`);

  constructor(private _cloudinary: CloudinaryService) {}

  ngOnInit(): void {
    const url = this._cloudinary.getImageUrl();
    console.log('url ', url);

    // this.images = this._cloudinary.getImageUrls("cid-sample")
  }
}
