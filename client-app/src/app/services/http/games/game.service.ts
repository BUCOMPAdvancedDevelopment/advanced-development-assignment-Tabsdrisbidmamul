import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject, Subject } from 'rxjs';
import { GameDTO, IGame } from 'src/app/interfaces/games.interface';
import { AgentService } from '../agent/agent.service';

@Injectable({
  providedIn: 'root',
})
export class GameService extends AgentService {
  gamesList$ = new ReplaySubject<IGame[]>(1);

  constructor(protected override _http: HttpClient) {
    super(_http);
  }

  storeGameList(gameList: IGame[]) {
    this.gamesList$.next(gameList);
  }

  getAllGames() {
    return this.get<GameDTO[]>('games');
  }
}
