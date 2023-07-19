import { Input } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { MatAccordion } from '@angular/material/expansion';
import { ICinema } from '../shared/models/ICinema';
import { IProjection } from '../shared/models/IProjection';
import { ProjectionService } from '../shared/services/projections.service';

@Component({
  selector: 'app-cinema-info',
  templateUrl: './cinema-info.component.html',
  styleUrls: ['./cinema-info.component.scss']
})
export class CinemaInfoComponent implements OnInit {
  @Input() projections: IProjection[]= [];
  @Input() cinema: ICinema | undefined;
  earliestProjection: IProjection | undefined;
  

  constructor(private projectionService: ProjectionService) { }

  ngOnInit(): void {
    console.log(this.projections);
    let earliest_index = undefined;
    let today = new Date();
    let earliest_time = undefined;
    for (let i = 0; i < this.projections.length; i++){
      let time = new Date(this.projections[i].dateTimeProjection);
      console.log("Ith time: " + time.getTime());
      console.log("Current time: " + today.getTime()); 
      if (time.getTime() >= today.getTime()){
        earliest_time = time;
        earliest_index = i;
        break;
      }
    }
    for (let i = 0; i < this.projections.length; i++){
      let time = new Date(this.projections[i].dateTimeProjection);
      if (time.getTime() >= today.getTime() && earliest_time!=undefined){
        if (time.getTime() <= earliest_time.getTime()){
          earliest_time = time;
          earliest_index = i;
        }
      }
    }
    if (earliest_index!=undefined){
      this.earliestProjection = this.projections[earliest_index];
    }
    console.log("Najranij  indeks: " + earliest_index);
    console.log("Najranija projekcija: " + JSON.stringify(this.earliestProjection));
  }

}
