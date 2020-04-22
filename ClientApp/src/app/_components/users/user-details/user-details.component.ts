import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { UserService } from "@app/_services";
import { Observable } from "rxjs";
import { User } from "@app/_models";
@Component({
  selector: "app-user-details",
  templateUrl: "./user-details.component.html",
  styleUrls: ["./user-details.component.less"],
})
export class UserDetailsComponent implements OnInit {
  public user: User;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService
  ) {}

  ngOnInit() {
    let id = parseInt(this.route.snapshot.paramMap.get("id"));
    this.userService
      .getById(id)
      .pipe()
      .subscribe((user) => {
        this.user = user;
      });
  }
}
