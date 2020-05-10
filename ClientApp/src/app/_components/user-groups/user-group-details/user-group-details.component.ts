import { Component, OnInit } from '@angular/core';
import { UserGroupService } from '@app/_services/user-group.service';
import { ActivatedRoute } from '@angular/router';
import { UserGroup } from '@app/_models/user-group';
import { AuthenticationService } from '@app/_services';

@Component({
  selector: 'app-user-group-details',
  templateUrl: './user-group-details.component.html',
  styleUrls: ['./user-group-details.component.less']
})
export class UserGroupDetailsComponent implements OnInit {
  private invitationComponentVisible: boolean = false;
  public userGroup: UserGroup;
  constructor(private userGroupService: UserGroupService, private route: ActivatedRoute, private authenticationService:AuthenticationService) { }

  ngOnInit() {
    if(this.authenticationService.currentUserValue.role == "Admin"){
      let id = parseInt(this.route.snapshot.paramMap.get("id"));

    this.userGroupService.getById(id).pipe().subscribe((userGroup) => {this.userGroup = userGroup})
    }
    
      this.userGroupService.getById(this.authenticationService.currentUserValue.group.id).pipe().subscribe((userGroup) => {
        this.userGroup = userGroup;
      })

    
  }

  invitationToggle(){
    this.invitationComponentVisible = !this.invitationComponentVisible;
  }

}
