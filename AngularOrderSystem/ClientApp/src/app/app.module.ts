import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { PrimeNgModule } from './primeng.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { OrderComponent } from './order/order.component';
import { OrderSearchComponent } from './ordersearch/ordersearch.component';
import { ProductComponent } from './product/product.component';
import { ProductSearchComponent } from './productsearch/productsearch.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    OrderComponent,
    ProductComponent,
    ProductSearchComponent,
    OrderSearchComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    PrimeNgModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: OrderComponent, pathMatch: 'full' },
      { path: 'product', component: ProductComponent },
      { path: 'order', component: OrderComponent },
      { path: 'ordersearch', component: OrderSearchComponent },
      { path: 'productsearch', component: ProductSearchComponent },
      { path: 'editproduct/:id', component: ProductComponent },
      { path: 'editorder/:id', component: OrderComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
