import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IGenre } from '../models/IGenre';
import { ObserversModule } from '@angular/cdk/observers';
import { IMailData } from '../models/IMailData';


const apiUrl=environment.apiUrl;

@Injectable({
    providedIn:"root"
})

export class MailService{

    constructor(
        private http:HttpClient
    ){}


    sendEmail(mailData:IMailData):Observable<any>{
        const url = `${apiUrl}api/mails/sendmail`;
        return this.http.post(url, mailData, {responseType: 'text'})
            .pipe(tap(() => console.log(mailData))
        );
    }
    
}
