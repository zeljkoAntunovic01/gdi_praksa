import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GenresComponent } from './genres/genres.component';
import {MatButtonModule} from '@angular/material/button';
import {MatTableModule} from '@angular/material/table';
import {HttpClientModule} from '@angular/common/http';
import { GenreUpdateComponent } from './genre-update/genre-update.component';
import {MatFormField, MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input'; 
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { FormsModule }   from '@angular/forms';
import { GenreAddComponent } from './genre-add/genre-add.component';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import { MoviesComponent } from './movies/movies.component';
import { MovieUpdateComponent } from './movie-update/movie-update.component';
import { MovieAddComponent } from './movie-add/movie-add.component';
import {AgmCoreModule} from '@agm/core';
import { MapComponent } from './map/map.component';
import { AgmSnazzyInfoWindowModule } from '@agm/snazzy-info-window';
import { CinemaInfoComponent } from './cinema-info/cinema-info.component';
import {MatExpansionModule} from '@angular/material/expansion';

import {MatList, MatListModule} from '@angular/material/list';

@NgModule({
  declarations: [
    AppComponent,
    GenresComponent,
    GenreUpdateComponent,
    GenreAddComponent,
    MoviesComponent,
    MovieUpdateComponent,
    MovieAddComponent,
    MapComponent,
    CinemaInfoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatTableModule,
    MatButtonModule,
    HttpClientModule,
    MatFormFieldModule,
    MatInputModule,
    BrowserAnimationsModule,
    FormsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    AgmCoreModule.forRoot({
      // please get your own API key here:
      // https://developers.google.com/maps/documentation/javascript/get-api-key?hl=en
      apiKey: 'AIzaSyAgXYS9LfL6mLEO7FkyycSUmHG1QPRQ2q0'
    }),
    AgmSnazzyInfoWindowModule,
    MatExpansionModule,
    MatListModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
