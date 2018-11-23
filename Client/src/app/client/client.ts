import {HttpClient, HttpHeaders, HttpResponse, HttpResponseBase} from '@angular/common/http';
import {Inject, Injectable, InjectionToken, Optional} from '@angular/core';
import {Observable, of as _observableOf, throwError as _observableThrow} from 'rxjs';
import {catchError as _observableCatch, mergeMap as _observableMergeMap} from 'rxjs/operators';

import {SwaggerException} from '../../exceptions/swagger.exception';
import {Account} from '../../models/account';


export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable()
export class AccountsClient {
  private http: HttpClient;
  private readonly baseUrl: string;
  protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

  constructor(@Inject(HttpClient)
                http: HttpClient,
              @Optional() @Inject(API_BASE_URL)
                baseUrl?: string) {
    this.http = http;
    this.baseUrl = baseUrl ? baseUrl : '';
  }

  getAll(): Observable<Account[] | null> {
    let url_ = this.baseUrl + '/api/Accounts';
    url_ = url_.replace(/[?&]$/, '');

    const options_: any = {
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({
        'Accept': 'text/json'
      })
    };

    return this.http.request('get', url_, options_)
      .pipe(_observableMergeMap((response_: any) => this.processGetAll(response_)))
      .pipe(_observableCatch((response_: any) => {
        if (response_ instanceof HttpResponseBase) {
          try {
            return this.processGetAll(<any>response_);
          } catch (e) {
            return <Observable<Account[] | null>><any>_observableThrow(e);
          }
        } else {
          return <Observable<Account[] | null>><any>_observableThrow(response_);
        }
      }));
  }

  protected processGetAll(response: HttpResponseBase): Observable<Account[] | null> {
    const status = response.status;
    const responseBlob = response instanceof HttpResponse
      ? response.body
      : (<any>response).error instanceof Blob
        ? (<any>response).error
        : undefined;

    const _headers: any = {};
    if (response.headers) {
      for (const key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }

    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
        let result200: any = null;
        const resultData200 = _responseText === ''
          ? null
          : JSON.parse(_responseText, this.jsonParseReviver);

        if (resultData200 && resultData200.constructor === Array) {
          result200 = [];
          for (const item of resultData200) {
            result200.push(Account.fromJS(item));
          }
        }

        return _observableOf(result200);
      }));
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(_observableMergeMap(_responseText =>
        throwException('An unexpected server error occurred.', status, _responseText, _headers)));
    }
    return _observableOf<Account[] | null>(<any>null);
  }

  add(account: Account): Observable<Account | null> {
    let url_ = this.baseUrl + '/api/Accounts';
    url_ = url_.replace(/[?&]$/, '');

    const content_ = JSON.stringify(account);

    const options_: any = {
      body: content_,
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'text/json'
      })
    };

    return this.http.request('post', url_, options_)
      .pipe(_observableMergeMap((response_: any) => this.processAdd(response_)))
      .pipe(_observableCatch((response_: any) => {
        if (response_ instanceof HttpResponseBase) {
          try {
            return this.processAdd(<any>response_);
          } catch (e) {
            return <Observable<Account | null>><any>_observableThrow(e);
          }
        } else {
          return <Observable<Account | null>><any>_observableThrow(response_);
        }
      }));
  }

  protected processAdd(response: HttpResponseBase): Observable<Account | null> {
    const status = response.status;
    const responseBlob = response instanceof HttpResponse
      ? response.body
      : (<any>response).error instanceof Blob
        ? (<any>response).error
        : undefined;

    const _headers: any = {};
    if (response.headers) {
      for (const key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }

    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
        let result200: any = null;
        const resultData200 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
        result200 = resultData200 ? Account.fromJS(resultData200) : <any>null;
        return _observableOf(result200);
      }));
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
        return throwException('An unexpected server error occurred.', status, _responseText, _headers);
      }));
    }
    return _observableOf<Account | null>(<any>null);
  }

  get(id: number): Observable<Account | null> {
    let url_ = this.baseUrl + '/api/Accounts/{id}';
    if (id === undefined || id === null) {
      throw new Error('The parameter \'id\' must be defined.');
    }
    url_ = url_.replace('{id}', encodeURIComponent('' + id));
    url_ = url_.replace(/[?&]$/, '');

    const options_: any = {
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({
        'Accept': 'text/json'
      })
    };

    return this.http.request('get', url_, options_).pipe(_observableMergeMap((response_: any) => this.processGet(response_)))
      .pipe(_observableCatch((response_: any) => {
        if (response_ instanceof HttpResponseBase) {
          try {
            return this.processGet(<any>response_);
          } catch (e) {
            return <Observable<Account | null>><any>_observableThrow(e);
          }
        } else {
          return <Observable<Account | null>><any>_observableThrow(response_);
        }
      }));
  }

  protected processGet(response: HttpResponseBase): Observable<Account | null> {
    const status = response.status;
    const responseBlob = response instanceof HttpResponse
      ? response.body
      : (<any>response).error instanceof Blob
        ? (<any>response).error
        : undefined;

    const _headers: any = {};
    if (response.headers) {
      for (const key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }

    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
        let result200: any = null;
        const resultData200 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
        result200 = resultData200 ? Account.fromJS(resultData200) : <any>null;
        return _observableOf(result200);
      }));
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob)
        .pipe(_observableMergeMap(_responseText =>
          throwException('An unexpected server error occurred.', status, _responseText, _headers)
        ));
    }
    return _observableOf<Account | null>(<any>null);
  }

  update(id: number, account: Account): Observable<Account | null> {
    let url_ = this.baseUrl + '/api/Accounts/{id}';
    if (id === undefined || id === null) {
      throw new Error('The parameter \'id\' must be defined.');
    }
    url_ = url_.replace('{id}', encodeURIComponent('' + id));
    url_ = url_.replace(/[?&]$/, '');

    const content_ = JSON.stringify(account);

    const options_: any = {
      body: content_,
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'text/json'
      })
    };

    return this.http.request('put', url_, options_).pipe(_observableMergeMap((response_: any) => this.processUpdate(response_)))
      .pipe(_observableCatch((response_: any) => {
        if (response_ instanceof HttpResponseBase) {
          try {
            return this.processUpdate(<any>response_);
          } catch (e) {
            return <Observable<Account | null>><any>_observableThrow(e);
          }
        } else {
          return <Observable<Account | null>><any>_observableThrow(response_);
        }
      }));
  }

  protected processUpdate(response: HttpResponseBase): Observable<Account | null> {
    const status = response.status;
    const responseBlob = response instanceof HttpResponse
      ? response.body
      : (<any>response).error instanceof Blob
        ? (<any>response).error
        : undefined;

    const _headers: any = {};
    if (response.headers) {
      for (const key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }

    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
        let result200: any = null;
        const resultData200 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
        result200 = resultData200 ? Account.fromJS(resultData200) : <any>null;
        return _observableOf(result200);
      }));
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob).pipe(_observableMergeMap(_responseText =>
        throwException('An unexpected server error occurred.', status, _responseText, _headers)
      ));
    }
    return _observableOf<Account | null>(<any>null);
  }

  getBySpecification(id: number | null | undefined, name: string | null | undefined,
                     availableFunds: number | null | undefined, balance: number | null | undefined,
                     hasCard: boolean | null | undefined): Observable<Account[] | null> {
    let url_ = this.baseUrl + '/api/Accounts/spec?';
    if (id !== undefined) {
      url_ += 'id=' + encodeURIComponent('' + id) + '&';
    }
    if (name !== undefined) {
      url_ += 'name=' + encodeURIComponent('' + name) + '&';
    }
    if (availableFunds !== undefined) {
      url_ += 'availableFunds=' + encodeURIComponent('' + availableFunds) + '&';
    }
    if (balance !== undefined) {
      url_ += 'balance=' + encodeURIComponent('' + balance) + '&';
    }
    if (hasCard !== undefined) {
      url_ += 'hasCard=' + encodeURIComponent('' + hasCard) + '&';
    }
    url_ = url_.replace(/[?&]$/, '');

    const options_: any = {
      observe: 'response',
      responseType: 'blob',
      headers: new HttpHeaders({
        'Accept': 'text/json'
      })
    };

    return this.http.request('get', url_, options_)
      .pipe(_observableMergeMap((response_: any) => this.processGetBySpecification(response_)))
      .pipe(_observableCatch((response_: any) => {
        if (response_ instanceof HttpResponseBase) {
          try {
            return this.processGetBySpecification(<any>response_);
          } catch (e) {
            return <Observable<Account[] | null>><any>_observableThrow(e);
          }
        } else {
          return <Observable<Account[] | null>><any>_observableThrow(response_);
        }
      }));
  }

  protected processGetBySpecification(response: HttpResponseBase): Observable<Account[] | null> {
    const status = response.status;
    const responseBlob = response instanceof HttpResponse
      ? response.body
      : (<any>response).error instanceof Blob
        ? (<any>response).error
        : undefined;

    const _headers: any = {};
    if (response.headers) {
      for (const key of response.headers.keys()) {
        _headers[key] = response.headers.get(key);
      }
    }

    if (status === 200) {
      return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
        let result200: any = null;
        const resultData200 = _responseText === '' ? null : JSON.parse(_responseText, this.jsonParseReviver);
        if (resultData200 && resultData200.constructor === Array) {
          result200 = [];
          for (const item of resultData200) {
            result200.push(Account.fromJS(item));
          }
        }
        return _observableOf(result200);
      }));
    } else if (status !== 200 && status !== 204) {
      return blobToText(responseBlob)
        .pipe(_observableMergeMap(_responseText =>
          throwException('An unexpected server error occurred.', status, _responseText, _headers)));
    }
    return _observableOf<Account[] | null>(<any>null);
  }
}

function throwException(message: string, status: number, response: string,
                        headers: { [key: string]: any; }, result?: any): Observable<any> {
  if (result !== null && result !== undefined) {
    return _observableThrow(result);
  } else {
    return _observableThrow(new SwaggerException(message, status, response, headers, null));
  }
}

function blobToText(blob: any): Observable<string> {
  return new Observable<string>((observer: any) => {
    if (!blob) {
      observer.next('');
      observer.complete();
    } else {
      const reader = new FileReader();
      reader.onload = event => {
        observer.next((<any>event.target).result);
        observer.complete();
      };
      reader.readAsText(blob);
    }
  });
}
