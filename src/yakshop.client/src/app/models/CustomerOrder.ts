export class CustomerOrder {
  customer: string;
  order: Order;

  constructor(customerName: string, milk: number, skins: number) {
    this.customer = customerName;
    this.order = new Order(milk, skins);
  }
}

export class Order {
  milk: number;
  skins: number;

  constructor(milk: number, skins: number) {
    this.milk = milk;
    this.skins = skins;
  }
}
