import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {
  FormControl,
  FormGroup,
  Validators,
} from "@angular/forms";
import { Attendee, Meeting, MeetingService } from '../services/meeting.service';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { Route } from '@angular/compiler/src/core';

@Component({
  selector: 'app-meetingmodify',
  templateUrl: './meetingmodify.component.html',
  styleUrls: ['./meetingmodify.component.css']
})
export class MeetingModifyComponent implements OnInit {

  dropdownList = [];
  selectedItems = [];
  dropdownSettings: IDropdownSettings;
  attendees: Attendee[];
  meeting: Meeting = undefined;

  title:string ="Add New Meeting Schedule";
  //Agenda="";
  //Schedule="";

  formPostMeeting: FormGroup;
  isError: boolean = false;
  msgError = "";
  id:number=0;

  constructor(private router: Router,private meetingService: MeetingService) {

    this.meetingService.getAttendeeList().subscribe((data: Attendee[]) => {
      this.attendees = data;
    });  

    var params =this.router.url.split("/", 3);
    if(params.length == 3)
    {
      this.title="Modify Meeting Schedule";
      this.id = Number(params[2]); 
      this.meetingService.getMeeting(this.id).subscribe((data: Meeting) => {
        this.meeting = data;
        //alert(data);
      });       
      //console.log(this.meeting);
    }

  }

  ngOnInit() {
    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'name',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: true
    };
    this.initForm();
  }

  onItemSelect(item: any) {
    console.log(item);
  }
  onSelectAll(items: any) {
    console.log(items);
  }

  initForm() {
    this.formPostMeeting = new FormGroup({
      Subject: new FormControl("", [Validators.required]),
      Agenda: new FormControl("", [Validators.required]),
      Schedule: new FormControl("", [Validators.required]),
      Attendees: new FormControl("", [Validators.required])
    });
  }

  MeetingModifyProcess() {
    this.isError = false;
    if (!this.formPostMeeting.valid) {
      this.isError = true;
      this.msgError = "Please enter all fields.";
    } else {
      //API call for login method
      this.meetingService.setMeeting(this.formPostMeeting.value).subscribe(
        (response) => alert(response),
        (error) => alert(error)
      );
    }
  }
}