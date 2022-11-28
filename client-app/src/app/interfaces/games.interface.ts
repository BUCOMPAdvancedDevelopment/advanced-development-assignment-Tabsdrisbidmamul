export interface GameDTO {
  id: string;
  title: string;
  url: string;
  publicId: string;
  description: string;
  category: string;
  price: number;
  stock: number;
  createdAt: string;
}

export interface IGame {
  id: string;
  title: string;
  url: string;
  publicId: string;
  description: string;
  category: string;
  price: number;
  stock: number;
  createdAt: Date;
}
