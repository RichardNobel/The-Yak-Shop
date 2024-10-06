import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ReactiveFormsModule } from '@angular/forms';

import { provideRouter } from '@angular/router';
import { routes } from './app.routes';

import { HomepageComponent } from './pages/homepage/homepage.component';
import { OrderFormComponent } from './pages/orderform/orderform.component';
import { OrderThankyouComponent } from './pages/order-thankyou/order-thankyou.component';

@NgModule({
  declarations: [
    AppComponent,
    HomepageComponent,
    OrderFormComponent,
    OrderThankyouComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule
  ],
  providers: [provideRouter(routes)],
  bootstrap: [AppComponent]
})
export class AppModule { }
