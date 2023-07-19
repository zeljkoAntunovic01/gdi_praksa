import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MovieService } from '../shared/services/movies.service';
import { Location } from '@angular/common';
import { IGenre } from '../shared/models/IGenre';
import { GenreUpdateComponent } from '../genre-update/genre-update.component';

@Component({
  selector: 'app-genres',
  templateUrl: './genres.component.html',
  styleUrls: ['./genres.component.scss']
})
export class GenresComponent implements OnInit {
  @ViewChild(GenreUpdateComponent) update!: GenreUpdateComponent;
  genres: IGenre[] = [];
  genreId: number | undefined;
  //sensorType: ISensorType | undefined;

  //add
  add: boolean = false;
  //edit
  edit: boolean = false;
  //sensorTypeId: number | undefined;

  //delete
  deleteId: number | undefined;
  delete: boolean = false;
  displayedColumns: string[] = ['id', 'name', 'update', 'delete'];

  constructor(private router: Router, private route: ActivatedRoute, private moviesService: MovieService, private location: Location,) { }


  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.moviesService.getGenres().subscribe({
      next: genres => {
        this.genres = genres;
        console.log('getGenres subscribe -> next notification: ' + JSON.stringify(this.genres));
      },
      error: err => {
        console.log('getGenres subscribe -> error notification: ' + err);
      },
      complete() {
        console.log('getGenres subscribe -> complete notification');
      }
    })
  }

  onUpdateClick(genreId: number) {
    console.log("Ovo je id: " + genreId);
    if (this.genreId == genreId && this.edit == true) {

      this.edit = false;

    }

    else {

      this.edit = true;
      this.add = false;
      this.genreId = genreId;
      if (this.update!=undefined){
        this.update.getGenreDetails(this.genreId);
      }
      

    }
  
  }
  refreshEvent() {
    this.edit = false;
    this.add = false;
    this.getData();

  }
  onDeleteClick(genreId:number){
    if (this.edit == true && this.genreId == genreId){

      this.edit = false;
      
    }
    this.moviesService.deleteGenre(genreId).subscribe(() => {
      const index = this.genres.indexOf(this.genres.find(x => x.id == genreId)!, 0);
      if (index > -1) {
        this.genres.splice(index, 1);
      }


      this.genres = [...this.genres];
    });

  }
  onAddClick(){
    if (this.add == true){
      this.add = false;
      this.edit = false;
    }else{
      this.add = true;
      this.edit = false;
    }
  }
    
  


}
