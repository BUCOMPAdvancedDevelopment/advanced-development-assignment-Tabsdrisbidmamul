import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IProfileEdit } from 'src/app/interfaces/profile.interface';
import { AgentService } from '../agent/agent.service';

@Injectable({
  providedIn: 'root',
})
export class ProfileEditService extends AgentService {
  constructor(protected override _http: HttpClient) {
    super(_http);
  }

  editDisplayName(displayName: string) {
    return this.post<IProfileEdit, IProfileEdit>('profile', { displayName });
  }
}
