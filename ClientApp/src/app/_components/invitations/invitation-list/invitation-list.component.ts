import { Component, OnInit } from '@angular/core';
import { UserService, AuthenticationService } from '@app/_services';
import { Invitation } from '@app/_models/invitation';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-invitation-list',
  templateUrl: './invitation-list.component.html',
  styleUrls: ['./invitation-list.component.less']
})
export class InvitationListComponent implements OnInit {
  invitations: Invitation[];

  constructor(private userService: UserService, private authenticationService: AuthenticationService, private route: ActivatedRoute) { }

  ngOnInit() {
    //this.userService.showAllInvitations(parseInt(this.route.snapshot.paramMap.get("userId"))).pipe().subscribe(invites => this.invitations = invites);
    this.userService.showAllInvitations(parseInt(this.route.snapshot.paramMap.get("userId"))).pipe().subscribe(invites => console.log(invites));
  }

  

}
