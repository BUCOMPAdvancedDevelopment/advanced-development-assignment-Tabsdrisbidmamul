import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject, Subject } from 'rxjs';
import {
  GameDTO,
  ICoverArt,
  IGame,
  IGameCreateDTO,
  IGameEditDTO,
} from 'src/app/interfaces/games.interface';
import { AgentService } from '../agent/agent.service';

/**
 * Game service to add, edit, delete, put games
 */
@Injectable({
  providedIn: 'root',
})
export class GameService extends AgentService {
  gamesList$ = new ReplaySubject<IGame[]>(1);
  selectedGame$ = new ReplaySubject<IGame>(1);

  constructor(protected override _http: HttpClient) {
    super(_http);
  }

  storeGameList(gameList: IGame[]) {
    this.gamesList$.next(gameList);
  }

  getAllGames() {
    return this.get<GameDTO[]>('games');
  }

  getGame(id: string) {
    return this.get<GameDTO>(`games/${id}`);
  }

  getAllCategoryTypes() {
    return this.get<string[]>('games/categories');
  }

  removeGame(id: string) {
    return this.delete<GameDTO[]>(`games/${id}`);
  }

  editGame(id: string, gameDTO: IGameEditDTO) {
    return this.patch<IGameEditDTO>(`games/${id}`, gameDTO);
  }

  createGame(gameDTO: IGameCreateDTO) {
    return this.post<IGameCreateDTO, GameDTO>('games', gameDTO);
  }

  addImage(formData: FormData) {
    return this.post<FormData, ICoverArt>(
      'photo/add-games-photo',
      formData,
      true
    );
  }
}
