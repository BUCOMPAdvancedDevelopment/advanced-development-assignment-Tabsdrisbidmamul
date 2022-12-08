import { Component, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { Basket, IBasket } from 'src/app/interfaces/basket.interface';
import { IGame } from 'src/app/interfaces/games.interface';
import { BasketService } from 'src/app/services/common/basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss'],
})
export class BasketComponent implements OnInit {
  private destroy$ = new Subject<void>();
  basketList: IBasket[] = [];
  total = 0;

  constructor(private _basketService: BasketService) {}

  ngOnInit(): void {
    this._basketService.list
      .pipe(takeUntil(this.destroy$))
      .subscribe((list) => {
        if (list !== null) {
          console.log('basket module list ', list);
          this.basketList = [];
          this.total = 0;

          list.forEach((item) => {
            let url = '';
            item.coverArt.forEach((image) => {
              if (image.isBoxArt) {
                url = image.url;
              }
            });

            this.total += item.price;

            const basketItem = new Basket(item.title, url, item.price);
            this.basketList.push(basketItem);
          });
        }
      });
  }

  removeItemFromBasket(index: number) {
    this._basketService.removeItem(index);
  }
}
