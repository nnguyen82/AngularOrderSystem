import { Component, Inject } from '@angular/core';
import { Observable } from "rxjs/Observable";
import { HttpClient } from '@angular/common/http';
import { Router } from "@angular/router";

@Component({
  selector: 'app-ordersearch',
  templateUrl: './ordersearch.component.html'
})
export class OrderSearchComponent {
  public orders: Order[] = [];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) {

    this.getAll().subscribe(res => {
      this.MapData(res);
    });

    setTimeout(() => {
      this.getAll().subscribe(res => {
        this.MapData(res);
      });
    }, 1000);
  }

  MapData(data: any) {
    this.orders = [];

    for (let d of data) {
      this.orders.push(d);
    }
  }

  getAll(): Observable<Order[]> {
    return this.http.get<Order[]>(this.Url + "GetAll");
  }

  Delete(id: any): Observable<{}> {
    let Url = this.Url + id;

    return this.http.delete(Url, { responseType: 'text' });
  }

  onEdit(index: any, order: any) {
    this.router.navigate(['/editorder', order.id]);
  }

  onDelete(index: any, order: any) {
    this.Delete(order.id).subscribe(res => {
      this.orders.splice(index, 1);
    }, err => { console.log("Delete Error: " + err.message) });
  }

  onNewOrder() {
    this.router.navigate(['/order']);
  }

  private Url = this.baseUrl + "api/Order/";
}

interface Order {
  id: any;
  description: string;
  orderDate: any;
  products: Product[];
}

interface Product {
  id: any;
  name: string;
  descriptions: string;
  price: any;
}
