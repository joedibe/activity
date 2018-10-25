import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Document } from 'src/domain/document';

@Injectable()

export class DocumentService {

  private documentUrl = "https://localhost:44331/api/Document";

  constructor(private http: HttpClient) { }

  getDocument() {
    return this.http.get<Document[]>(this.documentUrl)
      .toPromise()
      .then(data => {
        return data as Document[];
      });
  }

  postDocument(objEntity: Document) {
    return this.http.post<Document>(this.documentUrl, objEntity)
      .toPromise()
      .then(data => {
        return data as Document;
      });
  }

  putDocument(id, objEntity: Document) {
    return this.http.put<Document>(this.documentUrl + "/" + id, objEntity)
      .toPromise()
      .then(data => {
        return data as Document;
      });
  }

  deleteDocument(id) {
    return this.http.delete(this.documentUrl + "/" + id)
      .toPromise()
      .then(() => null);
  }
}
