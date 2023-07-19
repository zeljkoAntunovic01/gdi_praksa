import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges, Inject } from '@angular/core';
import { IGenre } from '../shared/models/IGenre';
import { IMailData } from '../shared/models/IMailData';
import { MailService } from '../shared/services/mailservice';
import {MovieService} from '../shared/services/movies.service';
@Component({
  selector: 'app-genre-update',
  templateUrl: './genre-update.component.html',
  styleUrls: ['./genre-update.component.scss']
})
export class GenreUpdateComponent implements OnInit {
  @Input() genreId: number | undefined;
  @Output() refreshEvent: EventEmitter<void> = new EventEmitter();
  genre: IGenre | undefined;
  mailData: IMailData={
    to: ['zeljko.ant123@gmail.com'],
    bcc:[],
    cc:[],
    from:'zeljko.ant123@gmail.com',
    displayName:'Željko Antunović',
    replyTo:'zeljko.ant123@gmail.com',
    replyToName:'Test Mail',
    subject:'Updated Genre',
    body:'Updated Genre:<br><br>'
  };
  constructor(private movieService: MovieService, private mailService: MailService) { }

  ngOnInit(): void {
    this.getGenreDetails(this.genreId!);
  }
  OnSubmit() {
    if (this.genre != undefined) {
      this.movieService.updateGenre(this.genre).subscribe(() => {
          this.refreshEvent.emit();
      });
      
      var genreBody = 'After the update: <br>';
      genreBody = genreBody.concat('ID: ' + this.genre.id + '<br>');
      genreBody = genreBody.concat('Name: ' + this.genre.name + '<br>');
      this.mailData.body = this.mailData.body.concat(genreBody);
      this.mailService.sendEmail(this.mailData).subscribe(() => {
        console.log(`Genre na mail: ${genreBody}`);
      })
    }
  }
  getGenreDetails(genreId: number) {
    if (genreId!=undefined){
        this.movieService.getGenre(genreId).subscribe((genre: IGenre) => {
          this.genre = genre;
          var genreBody = 'Before the update: <br>';
          genreBody = genreBody.concat('ID: ' + this.genre.id + '<br>');
          genreBody = genreBody.concat('Name: ' + this.genre.name + '<br><br>');
          this.mailData.body = this.mailData.body.concat(genreBody);
      })
    }
  }

}
