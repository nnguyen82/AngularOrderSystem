import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs/Observable";
import { Router } from "@angular/router";

@Component({
  selector: 'app-productsearch',
  templateUrl: './productsearch.component.html',
})

export class ProductSearchComponent {
  public products: Product[] = [];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) {
    this.getAll().subscribe(res => {
      this.products = res;
    })

    setTimeout(() => {
      this.getAll().subscribe(res => {
        this.products = res;
      });
    }, 1000);
  }

  private Url = this.baseUrl + "api/Product/";

  //This can be a service
  getAll(): Observable<Product[]> {
    return this.http.get<Product[]>(this.Url + "GetAll");
  }

  Delete(id: any): Observable<{}> {
    let Url = this.Url + id;

    return this.http.delete(Url, { responseType: 'text' });
  }

  public onDelete(index: any, p: any) {
    this.Delete(p.id).subscribe(res => {
      this.products.splice(index, 1);
    }, err => { console.log("Delete Error: " + err.message) });
  }

  public onEdit(index: any, p: any) {
    this.router.navigate(['/editproduct', p.id]);
  }

  public onNewProduct() {
    this.router.navigate(['/product']);
  }
}

interface Product {
  id: any;
  name: string;
  descriptions: string;
  price: any;
}
