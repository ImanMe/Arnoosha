import { IProductType } from './../shared/models/product-type';
import { IBrand } from './../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'https://localhost:44352/api/';

  constructor(private http: HttpClient) { }

  getProducts = (brandId?: number, productTypeId?: number, sort?: string, isSortAscending?: string) => {
    let params = new HttpParams();

    if (brandId) {
      params = params.append('brandId', brandId.toString());
    }

    if (productTypeId) {
      params = params.append('typeId', productTypeId.toString());
    }

    if (sort) {
      params = params.append('sortBy', sort);
    }

    if (isSortAscending) {
      params = params.append('isSortAscending', isSortAscending);
    }

    return this.http.get<IPagination>(`${this.baseUrl}products`, { observe: 'response', params })
      .pipe(
        map(response => {
          return response.body;
        })
      );
  }

  getBrands = () => {
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }

  getProductTypes = () => {
    return this.http.get<IProductType[]>(this.baseUrl + 'products/types');
  }
}
