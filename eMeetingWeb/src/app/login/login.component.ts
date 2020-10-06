import { AuthService } from "../services/auth.service";
import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from "@angular/forms";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  loginError: boolean = false;

  constructor(
    private router: Router,
    private authenticationService: AuthService
  ) {}

  public loginMessage = "";

  ngOnInit() {
    this.initForm();
  }

  initForm() {
    this.loginForm = new FormGroup({
      UserName: new FormControl("", [Validators.required]),
      Password: new FormControl("", [Validators.required]),
    });
  }

  LoginProcess() {
    this.loginError = false;

    if (!this.loginForm.valid) {
      this.loginError = true;
      this.loginMessage = "Please enter all fields.";
    } else {
      //API call for login method
      this.authenticationService.loginNow(this.loginForm.value).subscribe(
        (result) => {
          localStorage.removeItem("access_token");
          if (result.success) {
            localStorage.setItem("access_token", result.token);
            this.router.navigate(["/meeting"]);
          } else {
            this.loginMessage = "Error";
          }
        },
        (error) => {
          localStorage.removeItem("access_token");
          this.loginMessage = "Invalid Username/Password";
          this.loginError = true;
        }
      );
    }
  }
}
