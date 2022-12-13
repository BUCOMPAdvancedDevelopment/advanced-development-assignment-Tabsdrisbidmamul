import { Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

/**
 * Convert URLs to safe URLs
 */
@Pipe({
  name: 'safe',
})
export class SafePipe implements PipeTransform {
  constructor(private _sanitizer: DomSanitizer) {}

  transform(value: string, ...args: unknown[]): SafeResourceUrl {
    return this._sanitizer.bypassSecurityTrustResourceUrl(value);
  }
}
