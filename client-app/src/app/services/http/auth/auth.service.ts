import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { AgentService } from '../agent/agent.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService extends AgentService {
  user$ = new BehaviorSubject(null);

  constructor(protected override _http: HttpClient, private _router: Router) {
    super(_http);
  }
}
