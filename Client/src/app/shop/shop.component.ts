import { IProductType } from './../shared/models/product-type';
import { IBrand } from './../shared/models/brand';
import { ShopService } from './shop.service';
import { Component, OnInit } from '@angular/core';
import { IProduct } from '../shared/models/product';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products: IProduct[];
  brands: IBrand[];
  productTypes: IProductType[];
  brandIdSelected = 0;
  productTypeIdSelected = 0;
  sortSelected = 'name';
  isSortAscending = 'true';
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' }
  ];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getProductTypes();
  }

  getProducts = () => {
    this.shopService.getProducts(
      this.brandIdSelected
      , this.productTypeIdSelected
      , this.sortSelected
      , this.isSortAscending)
      .subscribe(response => {
        this.products = response.data;
      }, error => console.log(error));
  }

  getBrands = () => {
    this.shopService.getBrands().subscribe(response => {
      this.brands = [{ id: 0, name: 'All' }, ...response];
    }, error => console.log(error));
  }

  getProductTypes = () => {
    this.shopService.getProductTypes().subscribe(response => {
      this.productTypes = [{ id: 0, name: 'All' }, ...response];
    }, error => console.log(error));
  }

  onProductTypeSelected = (productTypeId: number) => {
    this.productTypeIdSelected = productTypeId;
    this.getProducts();
  }

  onBrandSelected = (brandId: number) => {
    this.brandIdSelected = brandId;
    this.getProducts();
  }

  onSortSelected = (sort: string) => {
    if (sort === 'priceAsc' || sort === 'priceDesc') {
      this.sortSelected = 'price';
    } else {
      this.sortSelected = sort;
    }
    if (sort === 'priceDesc') {
      this.isSortAscending = 'false';
    } else {
      this.isSortAscending = 'true';
    }

    console.log(this.isSortAscending);

    this.getProducts();
  }
}
