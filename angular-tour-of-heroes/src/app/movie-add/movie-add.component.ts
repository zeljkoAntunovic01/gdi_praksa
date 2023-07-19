import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { IMailData } from '../shared/models/IMailData';
import { IMovie } from '../shared/models/IMovie';

import { MailService } from '../shared/services/mailservice';
import { MovieService } from '../shared/services/movies.service';

@Component({
  selector: 'app-movie-add',
  templateUrl: './movie-add.component.html',
  styleUrls: ['./movie-add.component.scss']
})
export class MovieAddComponent implements OnInit {

  @Output() refreshEvent: EventEmitter<void> = new EventEmitter();
  movie:IMovie = {
    id: 0,
    title:"",
    releaseDate:"",
    genreId:0,
    genreName:"",
    runTime:0
  };
  mailData: IMailData={
    to: ['zeljko.ant123@gmail.com'],
    bcc:[],
    cc:[],
    from:'zeljko.ant123@gmail.com',
    displayName:'Željko Antunović',
    replyTo:'zeljko.ant123@gmail.com',
    replyToName:'Test Mail',
    subject:'Added Movie',
    body:'Added Movie:<br>'
  };

  constructor(private router: Router, private _httpClient: HttpClient, private movieService: MovieService, private mailService: MailService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
  }
  OnSubmit() {
    this.movieService.addMovie(this.movie).subscribe(() => {
      this.refreshEvent.emit();
    });
    var movieBody = '';
    movieBody = movieBody.concat('ID: ' + this.movie.id + '<br>');
    movieBody = movieBody.concat('Title: ' + this.movie.title + '<br>');
    movieBody = movieBody.concat('Genre: ' + this.movie.genreName + '<br>');
    movieBody = movieBody.concat('Release Date: ' + this.movie.releaseDate + '<br>');
    movieBody = movieBody.concat('Run Time: ' + this.movie.runTime + '<br><br>');
    this.mailData.body = this.mailData.body.concat(movieBody);
    this.mailService.sendEmail(this.mailData).subscribe(() => {
      console.log(`TV show na mail: ${movieBody}`);
    });
  }

}
