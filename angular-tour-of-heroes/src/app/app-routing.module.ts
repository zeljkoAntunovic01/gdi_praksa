import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GenresComponent } from './genres/genres.component';
import { MapComponent } from './map/map.component';
import { MoviesComponent } from './movies/movies.component';

const routes: Routes = [

  {path: '', redirectTo: '/genres',pathMatch:'full'},

  {path:'genres', component:GenresComponent  },
  {path:'movies', component:MoviesComponent},
  {path:'map', component:MapComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
