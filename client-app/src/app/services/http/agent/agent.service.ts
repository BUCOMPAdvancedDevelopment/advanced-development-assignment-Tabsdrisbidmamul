import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
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
    return this._http.put<any>(endpoint, putData, {
      headers: {
        useEndpoint,
      },
    });
  }

  protected post<TypeA, TypeB>(
    endpoint: string,
    postData: TypeA,
    submitAsForm?: boolean,
    useEndpoint = Endpoints.API
  ) {
    let httpCall: Observable<TypeB>;

    if (submitAsForm !== undefined) {
      httpCall = this._http.post<TypeB>(endpoint, postData, {
        headers: {
          useEndpoint,
          useForm: 'true',
        },
      });
    } else {
      httpCall = this._http.post<TypeB>(endpoint, postData, {
        headers: {
          useEndpoint,
        },
      });
    }
    return httpCall;
  }

  protected delete<Type>(
    endpoint: string,
    id?: string,
    useEndpoint = Endpoints.API
  ) {
    let httpCall: Observable<any>;

    if (id !== undefined) {
      httpCall = this._http.delete(`${endpoint}/${id}`, {
        headers: {
          useEndpoint,
        },
      });
    } else {
      httpCall = this._http.delete<Type>(`${endpoint}`, {
        headers: {
          useEndpoint,
        },
      });
    }

    return httpCall;
  }
}
