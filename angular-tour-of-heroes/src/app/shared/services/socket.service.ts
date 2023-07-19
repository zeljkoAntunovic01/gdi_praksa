import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import * as signalR from '@microsoft/signalr';
import { ISocketNotifyMessage } from '../models/ISocketNotifyMessage';


const apiUrl = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class SocketService {
  private hubConnection!: signalR.HubConnection;
  conectionAttempts: number = 0;
  isConnected = false;

  constructor() { }

  public buildConnection(): void {
    if (this.isConnected == false) {
      this.hubConnection = new signalR.HubConnectionBuilder().withUrl(`${apiUrl}appHub`).build();
      this.startConnection();

      this.hubConnection.on('camundaMessageHub',(data:ISocketNotifyMessage)=>{
        console.log(`socket message has been received: $(JSON.stringify(data)}`);
        alert(`socket message has been received: ${JSON.stringify(data)}`);
      });
    }
  }

  public startConnection = () => {
    this.conectionAttempts++;
    this.hubConnection.start()
      .then(() => {
        this.conectionAttempts = 0;
        this.isConnected = true;
        console.log("socket Connection has been started");
      })
      .catch(err=>{
        console.log("socket error while establishing connection");
        setTimeout(()=>{
          this.startConnection();
        },5000);
      })
  }
}
