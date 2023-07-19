import { Component, OnInit } from '@angular/core';
import { ICinema } from '../shared/models/ICinema';
import {IMarker} from '../shared/models/IMarker'
import { IProjection } from '../shared/models/IProjection';
import {ProjectionService} from '../shared/services/projections.service'

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit {
  // google maps zoom level
  zoom: number = 11;
  
  // initial center position for the map
  lat: number = 45.765399;
  lng: number = 16.016568;
  markers: IMarker[] = [];
  cinemas: ICinema[] | undefined;
  projectionsInCinema: IProjection[] = [];
  selectedCinema: ICinema | undefined;
  details: boolean = false;
  constructor(private projectionService: ProjectionService) { }

  ngOnInit(): void {
    this.getData();

  }
  getData(){
    this.projectionService.getCinemas().subscribe((cinemas:ICinema[])=>{
      this.cinemas = cinemas;
      console.log("DOHVACENA KINA: " + this.cinemas);
      this.markers = [];
      this.cinemas.forEach(c => {
        this.markers.push({
          lat: c.latitude,
          lng: c.longitude,
          label: "",
          draggable: false,
          id: c.id
        });
      });
    })

  }
  clickedMarker(id: number) {

    this.projectionService.getProjectionsInCinema(id).subscribe((projections: IProjection[]) => {
      this.projectionsInCinema = projections;
      this.details = true;
    });
    this.projectionService.getCinema(id).subscribe((cinema: ICinema) => {
      this.selectedCinema = cinema;
    });
    
  }
  mapClicked($event: google.maps.MouseEvent) {
    this.details = false;
    if ($event.latLng!= undefined){
      this.markers.push({
        lat: $event.latLng.lat(),
        lng: $event.latLng.lng(),
        draggable: true,
        id: 0
      });
    }
    
  }
  markerDragEnd(m: IMarker, $event: google.maps.MouseEvent) {
    console.log('dragEnd', m, $event);
  }
}
