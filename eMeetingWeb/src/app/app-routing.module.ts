import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { MeetingComponent } from './meeting/meeting.component';
import { MeetingModifyComponent } from './meetingmodify/meetingmodify.component';

const routes: Routes = [
  {
    path: "",
    redirectTo: "login",
    pathMatch: "full",
  },
  {
    path: "login",
    component: LoginComponent,
    data: {
      title: "Login - Meeting Scheduler",
      module: false,
    }
  },
  {
    path: "meeting",
    component: MeetingComponent,
    data: {
      title: "List - Meeting Scheduler",
      module: false,
    }
  },
  {
    path: "meetingmodify/:id",
    component: MeetingModifyComponent,
    data: {
      title: "Modify - Meeting Scheduler",
      module: false,
    }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
