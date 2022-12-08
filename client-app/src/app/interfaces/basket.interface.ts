export interface IBasket {
  title: string;
  url: string;
  price: number;
}

export class Basket implements IBasket {
  title: string;
  url: string;
  price: number;

  constructor(title: string, url: string, price: number) {
    this.title = title;
    this.url = url;
    this.price = price;
  }
}
