import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';

import { IGenre } from '../models/IGenre';

import { IMovie } from '../models/IMovie';


const apiUrl=environment.apiUrl;

@Injectable({
    providedIn:"root"
})

export class MovieService{

    constructor(
        private http:HttpClient
    ){}

    getMovies():Observable<IMovie[]>{
        const url = `${apiUrl}api/movies`;
        console.log(url);
        return this.http.get<IMovie[]>(url).pipe(tap(data=>{
            console.log('getMovies: ' + JSON.stringify(data));
        }));
    }
    addMovie(newMovie:IMovie):Observable<any>{
        const url = `${apiUrl}api/movies/add-movie`;
        return this.http.post<IMovie>(url, newMovie)
            .pipe(tap(() => console.log("added new Movie"))
        );
    }
    updateMovie(updatedMovie:IMovie):Observable<any>{
        const url = `${apiUrl}api/movies/update-movie`;
        console.log(updatedMovie);
        return this.http.put(url, updatedMovie)
            .pipe(tap(() => console.log("updated Movie"))
        );
    }
    deleteMovie(id:number):Observable<IMovie>{
        const url = `${apiUrl}api/movies/delete/${id}`;
        return this.http.delete<IMovie>(url);
    }

    getGenres():Observable<IGenre[]>{
        const url = `${apiUrl}api/genres`;
        console.log(url);
        return this.http.get<IGenre[]>(url).pipe(tap(data=>{
            console.log('getGenres: ' + JSON.stringify(data));
        }));
    }
    addGenre(newGenre:IGenre):Observable<any>{
        const url = `${apiUrl}api/genres/add-genre`;
        return this.http.post<IGenre>(url, newGenre);
    }
    updateGenre(updatedGenre:IGenre):Observable<any>{
        const url = `${apiUrl}api/genres/update-genre`;
        console.log(updatedGenre);
        return this.http.put(url, updatedGenre)
            .pipe(tap(() => console.log("updated  Genre"))
        );
    }
    deleteGenre(id:number):Observable<IGenre>{
        const url = `${apiUrl}api/genres/delete/${id}`;
        return this.http.delete<IGenre>(url);
    }
    getGenre(id:number):Observable<IGenre>{
        const url = `${apiUrl}api/genres/${id}`;
        console.log(url);
        return this.http.get<IGenre>(url).pipe(tap(data=>{
            console.log('getGenre: ' + JSON.stringify(data));
        }));
    }

    getMovie(id:number):Observable<IMovie>{
        const url = `${apiUrl}api/movies/${id}`;
        console.log(url);
        return this.http.get<IMovie>(url).pipe(tap(data=>{
            console.log('getMovie: ' + JSON.stringify(data));
        }));
    }

}
