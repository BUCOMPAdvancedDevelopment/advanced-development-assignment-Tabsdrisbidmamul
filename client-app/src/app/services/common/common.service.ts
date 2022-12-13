import { Injectable } from '@angular/core';
import { ReplaySubject, Subject } from 'rxjs';

/**
 * Anything to do with spinners, loaders etc
 */
@Injectable({
  providedIn: 'root',
})
export class CommonService {
  loader$ = new ReplaySubject<boolean>(1);
  message$ = new ReplaySubject<string>(1);
  icon$ = new ReplaySubject<string>(1);
  showSpinner$ = new ReplaySubject<boolean>(1);
  showInput$ = new ReplaySubject<boolean>(1);

  closeMenus$ = new Subject<boolean>();

  constructor() {}
}
