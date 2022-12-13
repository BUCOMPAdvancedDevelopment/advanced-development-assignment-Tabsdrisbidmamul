import { Injectable } from '@angular/core';
import { fill } from '@cloudinary/transformation-builder-sdk/actions/resize';
import { Cloudinary, CloudinaryImage } from '@cloudinary/url-gen';
import { autoGravity } from '@cloudinary/url-gen/qualifiers/gravity';
import { IGame } from 'src/app/interfaces/games.interface';
import { DeviceType } from 'src/app/types/deviceType';

/**
 * JS SDK to transform images
 */
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

  transformIdToUrl(game: IGame, deviceType: DeviceType, isBoxArt = false) {
    const imagePath = this.extractImagePath(game, isBoxArt);

    let publicId = '';

    // if (isBoxArt) {
    //   publicId = game.coverArt[1].publicId;
    // } else {
    //   publicId = game.coverArt[0].publicId;
    // }

    return this.transformImage(
      this.cld.image(`${imagePath}/${publicId}`),
      deviceType
    );
  }

  private transformImage(image: CloudinaryImage, device: DeviceType) {
    return image
      .resize(
        device === 'desktop'
          ? fill().width(1500).gravity(autoGravity())
          : fill().width(750).height(500).gravity(autoGravity())
      )
      .quality(70)
      .format('webp')
      .toURL();
  }

  /**
   * Image paths are in the original url, however they are further to the end of the string, and to get back a valid URL we need the image path for that to work
   * @param game
   * @param isBoxArt
   * @returns
   */
  private extractImagePath(game: IGame, isBoxArt = false) {
    let split: string[] = [];

    if (isBoxArt) {
      split = game.coverArt[1].url.split('/');
    } else {
      split = game.coverArt[0].url.split('/');
    }

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
