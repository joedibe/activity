import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from 'src/domain/category';

@Injectable()

export class CategoryService {

  private categoryUrl = "https://localhost:44331/api/Category";

  constructor(private http: HttpClient) { }

  getCategory() {
    return this.http.get<Category[]>(this.categoryUrl)
      .toPromise()
      .then(data => {
        return data as Category[];
      });
  }

  postCategory(objEntity: Category) {
    return this.http.post<Category>(this.categoryUrl, objEntity)
      .toPromise()
      .then(data => {
        return data as Category;
      });
  }

  putCategory(id, objEntity: Category) {
    return this.http.put<Category>(this.categoryUrl + "/" + id, objEntity)
      .toPromise()
      .then(data => {
        return data as Category;
      });
  }

  deleteCategory(id) {
    return this.http.delete(this.categoryUrl + "/" + id)
      .toPromise()
      .then(() => null);
  }
}
