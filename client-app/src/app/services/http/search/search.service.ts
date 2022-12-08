import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GameDTO } from 'src/app/interfaces/games.interface';
import { AgentService } from '../agent/agent.service';

@Injectable({
  providedIn: 'root',
})
export class SearchService extends AgentService {
  constructor(protected override _http: HttpClient) {
    super(_http);
  }

  searchGames(query: string) {
    return this.get<GameDTO[]>(`search/search-games/${query}`);
  }
}
