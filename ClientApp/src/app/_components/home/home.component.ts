import { Component } from "@angular/core";
import { first } from "rxjs/operators";

import { User } from "@app/_models";
import { UserService, AuthenticationService } from "@app/_services";
import { Observable } from "rxjs";

@Component({ templateUrl: "home.component.html" })
export class HomeComponent {
  userFirstName: string;

  constructor(
    private userService: UserService,
    private authenticationService: AuthenticationService
  ) {}

  ngOnInit() {
    this.userFirstName = this.authenticationService.currentUserValue.firstName;
  }
}
