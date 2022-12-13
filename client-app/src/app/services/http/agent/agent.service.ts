import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Endpoints } from 'src/app/types/endpoints';

/**
 * base class for http calls
 */
@Injectable({
  providedIn: 'root',
})
export class AgentService {
  constructor(protected _http: HttpClient) {}

  /**
   * Get data from endpoint
   * @param endpoint
   * @param useEndpoint
   * @returns
   */
  protected get<Type>(endpoint: string, useEndpoint = Endpoints.API) {
    return this._http.get<Type>(endpoint, {
      headers: {
        useEndpoint,
      },
    });
  }

  /**
   * Put data at endpoint
   * @param endpoint
   * @param putData
   * @param useEndpoint
   * @returns
   */
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

  /**
   * Patch data at endpoint
   * @param endpoint
   * @param patchData
   * @param useEndpoint
   * @returns
   */
  protected patch<Type>(
    endpoint: string,
    patchData: Type,
    useEndpoint = Endpoints.API
  ) {
    return this._http.patch<any>(endpoint, patchData, {
      headers: {
        useEndpoint,
      },
    });
  }

  /**
   * Post data at endpoint
   * @param endpoint
   * @param postData
   * @param submitAsForm
   * @param useEndpoint
   * @returns
   */
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

  /**
   * Delete data at endpoint
   * @param endpoint
   * @param id
   * @param useEndpoint
   * @returns
   */
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
