import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
} from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { TextDocument } from 'src/app/models/Document';

@Injectable({
  providedIn: 'root',
})
export class SearchService {
  private serverUri: string = 'https://localhost:44307/api/search';

  constructor(private http: HttpClient) {}

  /**
   * Send search request and return result.
   *
   * @param query Ex:"+a -b c".
   * @returns array of TextDocuments.
   */
  search(query: string): Observable<TextDocument[]> {
    const options = {
      params: new HttpParams({ fromString: `query=${query}` }),
    };
    return this.http.get<TextDocument[]>(this.serverUri, options);
  }
}
