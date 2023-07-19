import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges, Inject } from '@angular/core';

import {IMailData} from '../shared/models/IMailData';
import { MailService } from '../shared/services/mailservice';
import {MovieService} from '../shared/services/movies.service';

import { IMovie } from '../shared/models/IMovie';
@Component({
  selector: 'app-movie-update',
  templateUrl: './movie-update.component.html',
  styleUrls: ['./movie-update.component.scss']
})
export class MovieUpdateComponent implements OnInit {

  @Input() movieId: number | undefined;
  @Output() refreshEvent: EventEmitter<void> = new EventEmitter();
  movie: IMovie | undefined;
  mailData: IMailData={
    to: ['zeljko.ant123@gmail.com'],
    bcc:[],
    cc:[],
    from:'zeljko.ant123@gmail.com',
    displayName:'Željko Antunović',
    replyTo:'zeljko.ant123@gmail.com',
    replyToName:'Test Mail',
    subject:'Updated Movie',
    body:'Updated Movie:<br><br>'
  };
  date: Date | undefined;
  constructor(private movieService: MovieService, private mailService: MailService) { }

  ngOnInit(): void {
    this.getMovieDetails(this.movieId!);
  }
  getMovieDetails(movieId: number) {
    if (movieId!=undefined){
        this.movieService.getMovie(movieId).subscribe((movie: IMovie) => {
          
          this.movie = movie;
          var movieBody = 'Before the update: <br>';
          movieBody = movieBody.concat('ID: ' + this.movie.id + '<br>');
          movieBody = movieBody.concat('Title: ' + this.movie.title + '<br>');
          movieBody = movieBody.concat('Genre: ' + this.movie.genreName + '<br>');
          movieBody = movieBody.concat('Release Date: ' + this.movie.releaseDate + '<br>');
          movieBody = movieBody.concat('Run Time: ' + this.movie.runTime + '<br><br>');
          this.mailData.body = this.mailData.body.concat(movieBody);
          this.date = new Date(this.movie.releaseDate);
      })
    }
  
  }
  OnSubmit() {
    if (this.movie != undefined) {
        const month = (this.date!.getMonth() + 1).toString().padStart(2, '0'); // Adding 1 to the month since it is zero-based
        const day = this.date!.getDate().toString().padStart(2, '0');
        const year = this.date!.getFullYear().toString().slice(-2); // Extracting the last two digits of the year
        const formattedDate = `${month}/${day}/${year}`;
        this.movie.releaseDate = formattedDate;
        
        this.movieService.updateMovie(this.movie).subscribe(() => {
            this.refreshEvent.emit();
        });
        var movieBody = 'After the update: <br>';
        movieBody = movieBody.concat('ID: ' + this.movie.id + '<br>');
        movieBody = movieBody.concat('Title: ' + this.movie.title + '<br>');
        movieBody = movieBody.concat('Genre: ' + this.movie.genreName + '<br>');
        movieBody = movieBody.concat('Release Date: ' + this.movie.releaseDate + '<br>');
        movieBody = movieBody.concat('Run Time: ' + this.movie.runTime + '<br><br>');
        this.mailData.body = this.mailData.body.concat(movieBody);
        this.mailService.sendEmail(this.mailData).subscribe(() => {
        })

    this.getMovieDetails(this.movieId!);
    }
  }

}
