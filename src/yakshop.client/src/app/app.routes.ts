import { HomepageComponent } from './pages/homepage/homepage.component';
import { OrderThankyouComponent } from './pages/order-thankyou/order-thankyou.component';
import { OrderFormComponent } from './pages/orderform/orderform.component';

export const routes = [
  { path: '', component: HomepageComponent },
  { path: 'orderform', component: OrderFormComponent },
  { path: 'order-thankyou', component: OrderThankyouComponent },
];
