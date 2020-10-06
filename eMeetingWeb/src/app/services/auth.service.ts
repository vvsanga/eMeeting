import { environment } from "src/environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Router } from "@angular/router";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  constructor(private http: HttpClient, private router: Router) {}

  loginNow(data): Observable<any> {
    console.log(environment.baseUrl + "AppUser/Login");

    return this.http.post(environment.baseUrl + "AppUser/Login", data, {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "yourhostname:port"
      }),
    });
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

  logOutNow() {
    localStorage.removeItem("access_token");
    this.router.navigate(["/"]);
    this.http.post(environment.baseUrl + "AppUser/Logout", this.newHeaders());
  }
}
