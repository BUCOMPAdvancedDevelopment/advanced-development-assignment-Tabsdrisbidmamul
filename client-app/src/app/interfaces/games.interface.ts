export interface GameDTO {
  id: string;
  title: string;
  coverArt: Array<{
    url: string;
    publicId: string;
    isBoxArt: boolean;
  }>;
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
  coverArt: Array<{
    url: string;
    publicId: string;
    isBoxArt: boolean;
  }>;
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
  coverArt: Array<{
    url: string;
    publicId: string;
    isBoxArt: boolean;
  }>;
  description: string;
  category: string[];
  price: number;
  stock: number;
  youtubeLink: string;
}
