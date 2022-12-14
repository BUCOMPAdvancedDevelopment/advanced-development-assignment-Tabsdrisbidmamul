import { Pipe, PipeTransform } from '@angular/core';
import { IGame } from 'src/app/interfaces/games.interface';

/**
 * Return back image public id
 */
@Pipe({
  name: 'images',
})
export class ImagesPipe implements PipeTransform {
  transform(value: IGame[]): string[] {
    console.log('value ', value);
    // return value.map<string>((game) => game.coverArt.publicId);
    return [];
  }
}
