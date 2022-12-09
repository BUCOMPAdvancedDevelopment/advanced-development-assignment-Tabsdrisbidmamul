import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import { Observable } from 'rxjs';
import { GameDTO, IGame } from 'src/app/interfaces/games.interface';
import { GameService } from '../../services/http/games/game.service';

@Injectable({ providedIn: 'root' })
export class CatalogueResolverService implements Resolve<GameDTO[]> {
  constructor(private _gameService: GameService) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): GameDTO[] | Observable<GameDTO[]> | Promise<GameDTO[]> {
    return this._gameService.getAllGames();
  }
}
