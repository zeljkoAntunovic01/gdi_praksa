import { Component } from '@angular/core';
import { SocketService } from './shared/services/socket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Movies';

  constructor(private socketService:SocketService){}

  ngOnInit():void{
    this.socketService.buildConnection();
  }
}
