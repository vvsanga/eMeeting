import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs/internal/Observable";
import { environment } from "src/environments/environment";
import { AuthService } from "./auth.service";

@Injectable({
  providedIn: "root",
})
export class MeetingService {
  public meeting: Meeting;
  constructor(private http: HttpClient, private authService: AuthService, private router: Router) {
  }

  private newHeaders() {
    return {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("access_token"),
      }),
    };
  }

  public getMeetingList(): Observable<any> {
    return this.http.get(environment.baseUrl + "meeting", this.newHeaders());
  }

  public getMeeting(id): Observable<any> {
    return this.http.get(environment.baseUrl + "meeting/" + id, this.newHeaders());
  }

  public getAttendeeList(): Observable<any> {
    return this.http.get(environment.baseUrl + "attendee", this.newHeaders());
  }

  public getAttendee(id): Observable<any> {
    return this.http.get(environment.baseUrl + "attendee/" + id, this.newHeaders());
  }

  public setMeeting(meeting) {
    return this.http.post(environment.baseUrl + "meeting", meeting, this.newHeaders());
  }
}

export interface Meeting {
  Id: Number;
  Subject: string;
  Agenda: string;
  Schedule: string;
  Attendees:string;
}

export interface Attendee {
  Id: Number;
  Name: string;
}