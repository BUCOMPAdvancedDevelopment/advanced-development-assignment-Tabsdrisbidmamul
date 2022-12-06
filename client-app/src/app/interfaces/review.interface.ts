export interface IReviews {
  id: string;
  reviews: Array<IReview>;
}

export interface IReview {
  username: string;
  review: string;
  rating: string;
}
