import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IGame } from 'src/app/interfaces/games.interface';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  private _list: IGame[] = [];
  private _observableList = new BehaviorSubject<IGame[] | null>(null);

  get list() {
    return this._observableList.asObservable();
  }

  constructor() {}

  addItem(game: IGame) {
    this._list.push(game);
    this._observableList.next(this._list);
  }

  removeItem(index: number) {
    this._list.splice(index, 1);

    console.log('this._list ', this._list);

    this._observableList.next(this._list);
  }
}
