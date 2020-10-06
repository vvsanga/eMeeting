import { Component, OnInit } from "@angular/core";
import { Meeting } from '../services/meeting.service';
import { MeetingService } from '../services/meeting.service';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: "app-meeting",
  templateUrl: "./meeting.component.html",
  styleUrls: ["./meeting.component.css"],
})
export class MeetingComponent implements OnInit {
  public meetings: Meeting[];

  constructor(private router: Router, private meetingService: MeetingService, private authService: AuthService) {
  }

  ngOnInit() {
    this.meetingService.getMeetingList().subscribe((data: Meeting[]) => {
      console.log(data);
      this.meetings = data;
    });
  }

  public onLogout() {
    this.authService.logOutNow();
  }
}
