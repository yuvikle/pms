import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { Trade } from '../models/trade.model';
import { Position } from '../models/position.model';

@Injectable({ providedIn: 'root' })
export class TradeService {
  private apiUrl = 'https://localhost:44313/api/trades';

  constructor(private http: HttpClient) {}

  addTrade(trade: Trade): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}`, trade).pipe(
      catchError(err => {
        console.error('Add Trade Error:', err);
        return throwError(() => new Error('Failed to add trade.'));
      })
    );
  }

  getPositions(): Observable<Position> {
    return this.http.get<Position>(`${this.apiUrl}/positions`, {
      headers:{
        "Access-Control-Allow-Origin": "http://localhost:4200/"
      }
    }).pipe(
      catchError(err => {
        console.error('Get Positions Error:', err);
        return throwError(() => new Error('Failed to fetch positions.'));
      })
    );
  }
}
