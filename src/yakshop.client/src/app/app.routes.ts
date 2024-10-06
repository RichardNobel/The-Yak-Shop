import {Routes} from '@angular/router';
import { HomepageComponent } from './pages/homepage/homepage.component';
import { OrderFormComponent } from './pages/orderform/orderform.component';

export const routes = [
    {path: '', component: HomepageComponent},
    {path: 'orderform', component: OrderFormComponent}
];
