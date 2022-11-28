import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject, Subject } from 'rxjs';
import { GameDTO, IGame } from 'src/app/interfaces/games.interface';
import { AgentService } from '../agent/agent.service';

@Injectable({
  providedIn: 'root',
})
export class GameService extends AgentService {
  gamesList$ = new Subject<IGame[]>();

  constructor(protected override _http: HttpClient) {
    super(_http);
  }

  storeGameList(gameList: IGame[]) {
    this.gamesList$.next(gameList);
  }

  getAllGames() {
    this.get<GameDTO[]>('games').subscribe((games) => {
      const _games = games.map<IGame>((game) => {
        return {
          ...game,
          createdAt: new Date(game.createdAt),
        };
      });

      this.gamesList$.next(_games);
    });
  }
}
