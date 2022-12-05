export interface GameDTO {
  id: string;
  title: string;
  coverArt: Array<{
    url: string;
    publicId: string;
    isBoxArt: boolean;
  }>;
  description: string;
  category: string;
  price: number;
  stock: number;
  createdAt: string;
}

export interface IGame {
  id: string;
  title: string;
  coverArt: Array<{
    url: string;
    publicId: string;
    isBoxArt: boolean;
  }>;
  description: string;
  category: string;
  price: number;
  stock: number;
  createdAt: Date;
}
