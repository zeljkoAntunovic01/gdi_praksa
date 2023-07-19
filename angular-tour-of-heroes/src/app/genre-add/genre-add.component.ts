import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IGenre } from '../shared/models/IGenre';
import { IMailData } from '../shared/models/IMailData';
import { MailService } from '../shared/services/mailservice';
import { MovieService } from '../shared/services/movies.service';

@Component({
  selector: 'app-genre-add',
  templateUrl: './genre-add.component.html',
  styleUrls: ['./genre-add.component.scss']
})
export class GenreAddComponent implements OnInit {
  @Output() refreshEvent: EventEmitter<void> = new EventEmitter();
  genre:IGenre = {
    id: 0,
    name:""
  };
  mailData: IMailData={
    to: ['zeljko.ant123@gmail.com'],
    bcc:[],
    cc:[],
    from:'zeljko.ant123@gmail.com',
    displayName:'Željko Antunović',
    replyTo:'zeljko.ant123@gmail.com',
    replyToName:'Test Mail',
    subject:'Added new Genre',
    body:'Added Genre:<br><br>'
  };
  constructor(private router: Router, private _httpClient: HttpClient, private movieService: MovieService, private mailService: MailService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
  }
  OnSubmit() {
    this.movieService.addGenre(this.genre).subscribe(() => {
      this.refreshEvent.emit();
    });
    var genreBody = '';
    genreBody = genreBody.concat('ID: ' + this.genre.id + '<br>');
    genreBody = genreBody.concat('Name: ' + this.genre.name + '<br>');
    this.mailData.body = this.mailData.body.concat(genreBody);
    this.mailService.sendEmail(this.mailData).subscribe(() => {
      console.log(`Genre na mail: ${genreBody}`);
    })
  }

}
