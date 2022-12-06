import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IReviews } from 'src/app/interfaces/review.interface';
import { AgentService } from '../agent/agent.service';

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
}
