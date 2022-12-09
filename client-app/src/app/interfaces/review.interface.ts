export interface IReviews {
  id: string;
  reviews: Array<IReview>;
}

export interface IReview {
  username: string;
  review: string;
  rating: string;
}

export interface IReviewDTO {
  gameId: string;
  review: string;
  rating: string;
}

export class ReviewDTO implements IReviewDTO {
  gameId: string;
  review: string;
  rating: string;

  constructor(gameId: string, review: string, rating: string) {
    this.gameId = gameId;
    this.review = review;
    this.rating = rating;
  }
}
