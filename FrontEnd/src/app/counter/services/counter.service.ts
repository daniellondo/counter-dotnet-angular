import { Injectable } from '@angular/core';
import { environments } from 'src/environments/environments';
import { Counter } from '../interfaces/counter.interface';
import { Observable, Subject, catchError, throwError, delay, lastValueFrom } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { EndpointResponse } from '../interfaces/response.interface';

@Injectable({
  providedIn: 'root'
})

export class CounterService {
  private readonly API_URL = `${environments.baseUrl}Count`;
  private counter$ = new Subject<Counter>();
  private counter: Counter = {
    id: 0,
    count : 0,
  };;

  constructor(private http: HttpClient) {

  }

  getCounter(): void {
    if(this.counter.id === 0){
      this.addCounter(this.counter).then();
    }
    this.http.get<EndpointResponse>(`${this.API_URL}`)
    .subscribe((data) => {
      this.counter = data.result;
      this.counter$.next(this.counter);
    });
  }

  getCounter$(): Observable<Counter> {
    this.getCounter();
    return this.counter$.asObservable();
  }

  async addCounter(counter: Counter): Promise<boolean>{
    await lastValueFrom(this.http
      .post<EndpointResponse>(`${this.API_URL}`, counter)
      .pipe(catchError(this.handleError)));
    this.getCounter();
    return true;
  }

  async updateCounter(counter: Counter): Promise<boolean>{
    await lastValueFrom(this.http
      .put<EndpointResponse>(`${this.API_URL}`, counter)
      .pipe(catchError(this.handleError)));
    this.getCounter();
    return true;
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, body was: `,
        error.error
      );
    }
    // Return an observable with a user-facing error message.
    return throwError(
      () => new Error('Something bad happened; please try again later.')
    );
  }

}
