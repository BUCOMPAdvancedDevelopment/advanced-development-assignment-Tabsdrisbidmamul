import { Injectable } from '@angular/core';
import { fill } from '@cloudinary/transformation-builder-sdk/actions/resize';
import { Cloudinary, CloudinaryImage } from '@cloudinary/url-gen';
import { autoGravity } from '@cloudinary/url-gen/qualifiers/gravity';
import { IGame } from 'src/app/interfaces/games.interface';
import { DeviceType } from 'src/app/types/deviceType';

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

  transformIdsToUrls(publicIds: IGame[], deviceType: DeviceType) {
    return publicIds.map<string>((game) =>
      this.transformIdToUrl(game, deviceType)
    );
  }

  transformIdToUrl(game: IGame, deviceType: DeviceType) {
    const imagePath = this.extractImagePath(game);

    return this.transformImage(
      this.cld.image(`${imagePath}/${game.publicId}`),
      deviceType
    );
  }

  private transformImage(image: CloudinaryImage, device: DeviceType) {
    return image
      .resize(
        device === 'desktop'
          ? fill().width(1500)
          : fill().width(992).height(500)
      )
      .quality(70)
      .format('webp')
      .toURL();
  }

  private extractImagePath(game: IGame) {
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

    return split.slice(startIndex, endIndex + 1).join('/');
  }
}
