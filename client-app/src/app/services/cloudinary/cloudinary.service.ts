import { Injectable } from '@angular/core';
import { Cloudinary } from '@cloudinary/url-gen';

@Injectable({
  providedIn: 'root',
})
export class CloudinaryService {
  cld = new Cloudinary({
    cloud: {
      cloudName: 'drmofy8fr',
    },
  });

  constructor() {}

  getImageUrl() {
    const image = this.cld.image('zuk2vod0pylx9u3helyz');
    return image.toURL();
  }

  getImageUrls(baseName: string) {
    const imageArray: string[] = [];

    for (let i = 0; i < 2; i++) {
      const image = this.cld.image(`${baseName}${i + 2}`);
      imageArray.push(image.toURL());
    }

    return imageArray;
  }
}
