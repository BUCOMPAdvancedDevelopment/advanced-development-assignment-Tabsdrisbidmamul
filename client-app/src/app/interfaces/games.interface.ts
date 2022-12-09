export interface GameDTO {
  id: string;
  title: string;
  coverArt: ICoverArt[];
  description: string;
  category: string[];
  price: number;
  stock: number;
  createdAt: string;
  youtubeLink: string;
}

export interface IGame {
  id: string;
  title: string;
  coverArt: ICoverArt[];
  description: string;
  category: string[];
  price: number;
  stock: number;
  createdAt: Date;
  youtubeLink: string;
}

export interface IGameEditDTO {
  id: string;
  title: string;
  description: string;
  category: string[];
  price: number;
  youtubeLink: string;
}

export interface ICoverArt {
  id: string;
  publicId: string;
  url: string;
  isBoxArt: string;
}
