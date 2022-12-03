import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CommonService {
  loader$ = new Subject<boolean>();
  message$ = new Subject<string>();
  icon$ = new Subject<string>();
  showSpinner$ = new Subject<boolean>();

  constructor() {}
}
