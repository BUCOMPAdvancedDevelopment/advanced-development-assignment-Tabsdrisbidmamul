import { Injectable } from '@angular/core';
import { fill } from '@cloudinary/transformation-builder-sdk/actions/resize';
import { Cloudinary, CloudinaryImage } from '@cloudinary/url-gen';
import { autoGravity } from '@cloudinary/url-gen/qualifiers/gravity';
import { IGame } from 'src/app/interfaces/games.interface';

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

  transformIdsToUrls(publicIds: IGame[]) {
    return publicIds.map<string>((game) => {
      const split = game.url.split('/');

      let startIndex = 0;
      let endIndex = 1;

      const regex = new RegExp(/v\d+/);

      split.find((el, i) => {
        const match = regex.test(el);

        if (match) {
          startIndex = i + 1;
          endIndex = i + 2;
        }
      });

      return this.cld
        .image(`${split[startIndex]}/${split[endIndex]}/${game.publicId}`)
        .resize(fill().width('775').height(500).gravity(autoGravity()))
        .toURL();
    });
  }
}
