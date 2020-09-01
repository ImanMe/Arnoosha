import { ShopParams } from './../shared/models/shopParams';
import { IProductType } from './../shared/models/product-type';
import { IBrand } from './../shared/models/brand';
import { ShopService } from './shop.service';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { IProduct } from '../shared/models/product';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search', { static: false }) searchTerm: ElementRef;
  products: IProduct[];
  brands: IBrand[];
  productTypes: IProductType[];
  shopParams = new ShopParams();
  totalCount: number;
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
      this.shopParams)
      .subscribe(response => {
        this.products = response.data;
        this.shopParams.pageIndex = response.pageSize;
        this.shopParams.pageIndex = response.pageIndex;
        this.totalCount = response.count;
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
    if (this.shopParams.productTypeId !== productTypeId) {
      this.shopParams.productTypeId = productTypeId;
      this.shopParams.pageIndex = 1;
      this.getProducts();
    }
  }

  onBrandSelected = (brandId: number) => {
    if (this.shopParams.brandId !== brandId) {
      this.shopParams.brandId = brandId;
      this.shopParams.pageIndex = 1;
      this.getProducts();
    }
  }

  onSortSelected = (sort: string) => {
    if (sort === 'priceAsc' || sort === 'priceDesc') {
      this.shopParams.sort = 'price';
    } else {
      this.shopParams.sort = sort;
    }
    if (sort === 'priceDesc') {
      this.shopParams.isSortAscending = 'false';
    } else {
      this.shopParams.isSortAscending = 'true';
    }

    this.getProducts();
  }

  onPageChange = (event: any) => {
    if (this.shopParams.pageIndex !== event) {
      this.shopParams.pageIndex = event;
      this.getProducts();
    }
  }

  onSearch = () => {
    if (this.shopParams.search !== this.searchTerm.nativeElement.value) {
      this.shopParams.search = this.searchTerm.nativeElement.value;
      this.getProducts();
    }
  }

  onReset = () => {
    if (JSON.stringify(this.shopParams) !== JSON.stringify(new ShopParams())) {
      this.searchTerm.nativeElement.value = '';
      this.shopParams = new ShopParams();
      this.getProducts();
    }
  }
}
