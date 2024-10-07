import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { CustomerOrder } from '../../models/CustomerOrder';
import { StockInfo } from '../../models/StockInfo';
import { Router } from '@angular/router';
import { SignalrService } from '../../services/signalr.service';

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

  constructor(private readonly router: Router, 
    private readonly formBuilder: FormBuilder, 
    private readonly http: HttpClient,
    public signalRService: SignalrService
  ) {}

  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.addRealTimeStockDataListener();
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

  onSubmit() {
    const order = new CustomerOrder(this.customerName.value!, this.milkQuantity.value!, this.skinsQuantity.value!);
    this.http.post<CustomerOrder>(`/yak-shop/order/${this.signalRService.stockInfo.dayNumber}`, order).subscribe({
      next: (result) => {
        // TODO: Make the user aware (on the "Thank You" page?) if an order can only be partially fulfilled.
        this.router.navigate(['order-thankyou']);
      },
      error: (error: any) => {
        // TODO: Notify customer if order placement fails (e.g. because of insufficient stock)
        console.error(error);
      },
      complete: () => {},  
    });
  }
}
