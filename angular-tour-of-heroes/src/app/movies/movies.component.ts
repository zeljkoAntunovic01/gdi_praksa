import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MovieService } from '../shared/services/movies.service';
import { Location } from '@angular/common';

import { MovieUpdateComponent } from '../movie-update/movie-update.component';
import { IMovie } from '../shared/models/IMovie';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.scss']
})
export class MoviesComponent implements OnInit {

  @ViewChild(MovieUpdateComponent) update!: MovieUpdateComponent;
  movies: IMovie[] = [];
  movieId: number | undefined;
  add: boolean = false;
  //edit
  edit: boolean = false;
  displayedColumns: string[] = ['id', 'title', 'Genre', 'ReleaseDate', 'runtime', 'update', 'delete'];
  constructor(private router: Router, private route: ActivatedRoute, private movieService: MovieService, private location: Location,) { }

  ngOnInit(): void {
    this.getData();
  }
  getData() {
    this.movieService.getMovies().subscribe({
      next: movies => {
        this.movies = movies;
        console.log('getGenres subscribe -> next notification: ' + JSON.stringify(this.movies));
      },
      error: err => {
        console.log('getGenres subscribe -> error notification: ' + err);
      },
      complete() {
        console.log('getGenres subscribe -> complete notification');
      }
    })
  }
  onUpdateClick(movieId: number) {
    console.log("Ovo je id: " + movieId);
    if (this.movieId == movieId && this.edit == true) {
      this.edit = false;
    }
    else {
      this.edit = true;
      this.add = false;
      this.movieId = movieId;
      if (this.update!=undefined){
        this.update.getMovieDetails(this.movieId);
      }
    }

  }
  
  
  refreshEvent() {
    this.edit = false;
    this.add = false;
    this.getData();

  }
  onDeleteClick(movieId:number){
    if (this.edit == true && this.movieId== movieId){

      this.edit = false;
      
    }
    this.movieService.deleteMovie(movieId).subscribe(() => {
      const index = this.movies.indexOf(this.movies.find(x => x.id == movieId)!, 0);
      if (index > -1) {
        this.movies.splice(index, 1);
      }


      this.movies = [...this.movies];
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
