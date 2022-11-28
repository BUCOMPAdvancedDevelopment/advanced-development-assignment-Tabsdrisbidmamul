import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Endpoints } from 'src/app/types/endpoints';

@Injectable({
  providedIn: 'root',
})
export class AgentService {
  constructor(protected _http: HttpClient) {}

  protected get<Type>(endpoint: string, useEndpoint = Endpoints.API) {
    return this._http.get<Type>(endpoint, {
      headers: {
        useEndpoint,
      },
    });
  }

  protected put<Type>(
    endpoint: string,
    putData: Type,
    useEndpoint = Endpoints.API
  ) {
    return this._http.put<Type>(endpoint, putData, {
      headers: {
        useEndpoint,
      },
    });
  }

  protected post<TypeA, TypeB>(
    endpoint: string,
    postData: TypeA,
    useEndpoint = Endpoints.API
  ) {
    return this._http.post<TypeB>(endpoint, postData, {
      headers: {
        useEndpoint,
      },
    });
  }

  protected delete(endpoint: string, id: string, useEndpoint = Endpoints.API) {
    return this._http.delete(`${endpoint}/${id}`, {
      headers: {
        useEndpoint,
      },
    });
  }
}
