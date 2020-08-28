import { ShopParams } from './../shared/models/shopParams';
import { IProductType } from './../shared/models/product-type';
import { IBrand } from './../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'https://localhost:44352/api/';

  constructor(private http: HttpClient) { }

  getProducts = (shopParams: ShopParams) => {
    let params = new HttpParams();

    if (shopParams.brandId !== 0) {
      params = params.append('brandId', shopParams.brandId.toString());
    }

    if (shopParams.productTypeId !== 0) {
      params = params.append('typeId', shopParams.productTypeId.toString());
    }

    params = params.append('sortBy', shopParams.sort);

    params = params.append('isSortAscending', shopParams.isSortAscending);

    params = params.append('pageSize', shopParams.pageSize.toString());

    params = params.append('pageIndex', shopParams.pageIndex.toString());

    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }

    return this.http.get<IPagination>(`${this.baseUrl}products`, { observe: 'response', params })
      .pipe(
        map(response => {
          return response.body;
        })
      );
  }

  getProduct = (id: number) => {
    return this.http.get<IProduct>(this.baseUrl + 'products/' + id)
  }

  getBrands = () => {
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }

  getProductTypes = () => {
    return this.http.get<IProductType[]>(this.baseUrl + 'products/types');
  }
}
