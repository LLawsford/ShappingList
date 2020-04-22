import { Component, OnInit } from "@angular/core";
import { UserService, AuthenticationService } from "@app/_services";
import { User } from "@app/_models";
import { Observable } from "rxjs";
import { first } from "rxjs/operators";
import { ActivatedRoute, Router } from "@angular/router";

@Component({
  selector: "app-user-list",
  templateUrl: "./user-list.component.html",
  styleUrls: ["./user-list.component.less"],
})
export class UserListComponent implements OnInit {
  users: User[] = [];

  constructor(private userService: UserService, private router: Router) {}

  ngOnInit() {
    this.userService
      .getAll()
      .pipe(first())
      .subscribe((users) => {
        this.users = users;
      });
  }

  onSelect(user) {
    this.router.navigate(["users", user.id]);
  }
}
