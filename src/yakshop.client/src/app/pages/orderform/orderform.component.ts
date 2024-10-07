import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { CustomerOrder } from '../../models/CustomerOrder';
import { StockInfo } from '../../models/StockInfo';
import { Router } from '@angular/router';

@Component({
  selector: 'app-orderform',
  templateUrl: './orderform.component.html',
  styleUrls: ['./orderform.component.css'],
})
export class OrderFormComponent implements OnInit {
  stockInfo: StockInfo | null = null;

  orderForm = this.formBuilder.group({
    customerName: ['', [Validators.required, Validators.minLength(2)]],
    milkQuantity: [1, Validators.required],
    skinsQuantity: [1, Validators.required],
  });

  constructor(private readonly router: Router, private readonly formBuilder: FormBuilder, private readonly http: HttpClient) {}

  ngOnInit() {
    this.getStockInfo();
  }

  get customerName() {
    return this.orderForm.get('customerName')!;
  }

  get milkQuantity() {
    return this.orderForm.get('milkQuantity')!;
  }

  get skinsQuantity() {
    return this.orderForm.get('skinsQuantity')!;
  }

  getStockInfo() {
    this.http.get<StockInfo>('/yak-shop/current-stock').subscribe({
      next: (result) => {
        this.stockInfo = result;
      },
      error: (error: any) => {
        console.error(error);
      },
      complete: () => {},
    });
  }

  onSubmit() {
    const order = new CustomerOrder(this.customerName.value!, this.milkQuantity.value!, this.skinsQuantity.value!);
    this.http.post<CustomerOrder>('/yak-shop/order/1', order).subscribe({
      next: (result) => {
        this.router.navigate(['order-thankyou']);
      },
      error: (error: any) => {
        console.error(error);
      },
      complete: () => {},  
    });
  }
}
