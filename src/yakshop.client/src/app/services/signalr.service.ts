import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { StockInfo } from '../models/StockInfo';

@Injectable({
  providedIn: 'root',
})
export class SignalrService {
  public stockInfo: StockInfo = {dayNumber: 0, milk: 0, skins: 0} as StockInfo;

  private hubConnection!: signalR.HubConnection;

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7051/realtimehub')
      .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch((err) => console.log('Error while starting connection: ' + err));
  };

  public addRealTimeStockDataListener = () => {
    this.hubConnection.on('receivestockdata', (data) => {
      this.stockInfo = data;
      console.log(data);
    });
  }
}
