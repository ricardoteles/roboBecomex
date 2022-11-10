import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiRobotResponse } from '../response/apiRobotResponse.model';

@Injectable()
export class RobotService {
  host = "https://localhost:7156/api/v1";

  constructor(private http: HttpClient) { }

  getInitialRobotState(): Observable<ApiRobotResponse> {
    return this.http.get<ApiRobotResponse>(
      `${this.host}/robot/initialState`);
  }

  moveRobotElbow(side: string, state: string): Observable<ApiRobotResponse> {
    const body = { state: state, side: side };

    return this.http.put<ApiRobotResponse>(
      `${this.host}/robot/move/elbow`, body);
  }

  rotateRobotWrist(side: string, state: string): Observable<ApiRobotResponse> {
    const body = { state: state, side: side };
    return this.http.put<ApiRobotResponse>(
      `${this.host}/robot/rotate/wrist`, body);
  }

  moveRobotHead(state: string): Observable<ApiRobotResponse> {
    const body = { state: state };
    return this.http.put<ApiRobotResponse>(
      `${this.host}/robot/move/head`, body);
  }

  rotateRobotHead(state: string): Observable<ApiRobotResponse> {
    const body = { state: state };
    return this.http.put<ApiRobotResponse>(
      `${this.host}/robot/rotate/head`, body);
  }
}
