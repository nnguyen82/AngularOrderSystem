import { Component, Inject } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Route } from "@angular/router";
import { Observable } from "rxjs/Observable";
import { Router } from "@angular/router";

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
})

export class ProductComponent {
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private activatedRoute: ActivatedRoute, private router: Router) {
        this.activatedRoute.paramMap.subscribe(param => {
            const productId = param.get('id');
            this.ProductId = null;

            if (productId !== null) {
                this.getProduct(productId).subscribe(res => {
                    this.editProduct(res);
                });
            }
        });
    }

    productGroup = new FormGroup({
        Name: new FormControl(''),
        Descriptions: new FormControl(''),
        Price: new FormControl(''),
    });

    editProduct(product: any) {
        this.productGroup.patchValue({
            Name: product[0].name,
            Descriptions: product[0].descriptions,
            Price: product[0].price
        })

        this.ProductId = product[0].id;
    }

    onSubmit() {
        let postData = {
            Id: this.ProductId,
            Name: this.productGroup.value.Name,
            Descriptions: this.productGroup.value.Descriptions,
            Price: this.productGroup.value.Price
        };

        if (this.ProductId !== null) {
            this.http.put(this.Url, postData, { responseType: 'text' }).subscribe(
                data => {
                    this.router.navigate(['/productsearch']);
                },
                err => {
                    console.log("Post error occured: " + err.message);
                }
            );
        }
        else {
            this.http.post(this.Url, postData, { responseType: 'text' }).subscribe(
                data => {
                    this.router.navigate(['/productsearch']);
                },
                err => {
                    console.log("Post error occured: " + err.message);
                }
            );
        }
    };

    getProduct(id: any): Observable<{}> {
        let Url = this.Url + "/" + id;

        return this.http.get(Url);
    }

    private Url = this.baseUrl + "api/Product";
    private ProductId: any;
}
