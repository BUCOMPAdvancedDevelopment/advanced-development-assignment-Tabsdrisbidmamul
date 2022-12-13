import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IReviewDTO, IReviews } from 'src/app/interfaces/review.interface';
import { AgentService } from '../agent/agent.service';

/**
 * Helper service to get reviews from API
 */
@Injectable({
  providedIn: 'root',
})
export class ReviewService extends AgentService {
  constructor(protected override _http: HttpClient) {
    super(_http);
  }

  getReviews(gameId: string) {
    return this.get<IReviews>(`firestore/${gameId}`);
  }

  addReview(reviewDTO: IReviewDTO) {
    return this.post<IReviewDTO, null>('firestore', reviewDTO);
  }
}
