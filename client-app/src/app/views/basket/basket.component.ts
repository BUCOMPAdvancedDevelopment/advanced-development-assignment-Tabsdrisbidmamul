import { Component, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { IGame } from 'src/app/interfaces/games.interface';
import { BasketService } from 'src/app/services/common/basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss'],
})
export class BasketComponent implements OnInit {
  private destroy$ = new Subject<void>();
  list: IGame[] = [];

  constructor(private _basketService: BasketService) {}

  ngOnInit(): void {
    this._basketService.list
      .pipe(takeUntil(this.destroy$))
      .subscribe((list) => {
        if (list !== null) {
          this.list = list;
          console.log('list ', list);
        }
      });
  }
}
