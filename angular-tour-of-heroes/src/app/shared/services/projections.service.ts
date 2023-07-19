import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';

import { ICinema } from '../models/ICinema';

import { IProjection } from '../models/IProjection';
import { IProjectionType } from '../models/IProjectionType';


const apiUrl=environment.apiUrl;

@Injectable({
    providedIn:"root"
})

export class ProjectionService{

    constructor(
        private http:HttpClient
    ){}

    getProjections():Observable<IProjection[]>{
        const url = `${apiUrl}api/projections`;
        console.log(url);
        return this.http.get<IProjection[]>(url).pipe(tap(data=>{
            console.log('getProjections: ' + JSON.stringify(data));
        }));
    }
    getProjectionsInCinema(id:number):Observable<IProjection[]>{
        const url = `${apiUrl}api/cinemas/projections/${id}`;
        return this.http.get<IProjection[]>(url).pipe(tap(data => {
            console.log("Projections in cinema " + JSON.stringify(data));
        }));
    }
    addProjection(newProjection:IProjection):Observable<any>{
        const url = `${apiUrl}api/projections/add-projection`;
        return this.http.post<IProjection>(url, newProjection)
            .pipe(tap(() => console.log("added new Projection"))
        );
    }
    updateProjection(updatedProjection:IProjection):Observable<any>{
        const url = `${apiUrl}api/projections/update-projection`;
        console.log(updatedProjection);
        return this.http.put(url, updatedProjection)
            .pipe(tap(() => console.log("updated Projection"))
        );
    }
    deleteProjection(id:number):Observable<IProjection>{
        const url = `${apiUrl}api/projections/delete/${id}`;
        return this.http.delete<IProjection>(url);
    }

    getCinemas():Observable<ICinema[]>{
        const url = `${apiUrl}api/cinemas`;
        console.log(url);
        return this.http.get<ICinema[]>(url).pipe(tap(data=>{
            console.log('getCinemas: ' + JSON.stringify(data));
        }));
    }
    addCinema(newCinema:ICinema):Observable<any>{
        const url = `${apiUrl}api/cinemas/add-cinema`;
        return this.http.post<ICinema>(url, newCinema);
    }
    updateCinema(updatedCinema:ICinema):Observable<any>{
        const url = `${apiUrl}api/cinemas/update-cinema`;
        console.log(updatedCinema);
        return this.http.put(url, updatedCinema)
            .pipe(tap(() => console.log("updated  Cinema"))
        );
    }
    deleteCinema(id:number):Observable<ICinema>{
        const url = `${apiUrl}api/cinemas/delete/${id}`;
        return this.http.delete<ICinema>(url);
    }
    getCinema(id:number):Observable<ICinema>{
        const url = `${apiUrl}api/cinemas/${id}`;
        console.log(url);
        return this.http.get<ICinema>(url).pipe(tap(data=>{
            console.log('getCinema: ' + JSON.stringify(data));
        }));
    }

    getProjection(id:number):Observable<IProjection>{
        const url = `${apiUrl}api/projections/${id}`;
        console.log(url);
        return this.http.get<IProjection>(url).pipe(tap(data=>{
            console.log('getProjection: ' + JSON.stringify(data));
        }));
    }
    getProjectionTypes():Observable<IProjectionType[]>{
        const url = `${apiUrl}api/projectiontypes`;
        console.log(url);
        return this.http.get<IProjectionType[]>(url).pipe(tap(data=>{
            console.log('getProjectionTypes: ' + JSON.stringify(data));
        }));
    }

}
