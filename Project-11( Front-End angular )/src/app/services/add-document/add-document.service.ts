import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TextDocument } from 'src/app/models/Document';

@Injectable({
  providedIn: 'root',
})
export class AddDocumentService {
  private serverUri: string = 'https://localhost:44307/api/search';
  constructor(private http: HttpClient) {}

  Add(textDocument: TextDocument) {
    this.http.post<TextDocument>(this.serverUri, textDocument);
  }
}
