import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Route } from "@angular/router";
import { Observable } from "rxjs/Observable";
import { Router } from "@angular/router";

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
})

export class OrderComponent {
  ProductSearchText: any;
  OrderDescription: any;
  OrderDate: any;
  public products: Product[] = [];
  disableAdd: boolean = false;
  private OrderId: any;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private activatedRoute: ActivatedRoute, private router: Router) {
    this.activatedRoute.paramMap.subscribe(param => {
      const orderId = param.get('id');
      this.OrderId = null;

      if (orderId !== null) {
        this.getOrder(orderId).subscribe(res => {
          this.editOrder(res);
        });
      }
    });
  }

  getOrder(id: any): Observable<{}> {
    let Url = this.Url + "/" + id;

    return this.http.get(Url);
  }

  editOrder(order: any) {
    this.OrderDescription = order[0].description;
    this.OrderDate = new Date(order[0].orderDate);
    this.OrderId = order[0].id;

    for (let p of order[0].products) {
      this.products.push(p);
    }
  }

  onSearch() {
    this.disableAdd = true;

    this.getProductByName(this.ProductSearchText).subscribe(res => {
      let prod: Product = {
        id: res[0].id,
        name: res[0].name,
        descriptions: res[0].descriptions,
        price: res[0].price
      };

      this.products.push(prod);
      this.disableAdd = false;
    },
      err => {
        console.log("Get error occured: " + err.message);
      });
  }

  onSubmit() {
    let postData = {
      Id: this.OrderId,
      Description: this.OrderDescription,
      OrderDate: this.OrderDate,
      Products: [] = this.products
    };

    if (this.OrderId !== null) {
      this.http.put(this.Url, postData, { responseType: 'text' }).subscribe(
        data => {
          this.router.navigate(['/ordersearch']);
        },
        err => {
          console.log("Post error occured: " + err.message);
        }
      );
    }
    else {
      this.http.post(this.Url, postData, { responseType: 'text' }).subscribe(
        data => {
          this.router.navigate(['/ordersearch']);
        },
        err => {
          console.log("Post error occured: " + err.message);
        }
      );
    }
  }

  onProductDelete(index: any) {
    this.products.splice(index, 1);
  }

  getProductByName(name: any): Observable<Product[]> {
    let Url = this.UrlProduct + "GetByName?name=" + name;

    return this.http.get<Product[]>(Url);
  }

  private UrlProduct = this.baseUrl + "api/Product/";
  private Url = this.baseUrl + "api/Order/";
}

interface Product {
  id: any;
  name: string;
  descriptions: string;
  price: any;
}
