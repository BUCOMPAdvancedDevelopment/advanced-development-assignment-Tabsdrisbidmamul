import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IProfileEdit } from 'src/app/interfaces/profile.interface';
import { IImage } from 'src/app/interfaces/user.interface';
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

  deleteImage() {
    return this.delete('photo');
  }

  addImage(formData: FormData) {
    return this.post<FormData, IImage>('photo', formData, true);
  }
}
