import { Component, OnInit } from '@angular/core';
import { UserGroup } from '@app/_models/user-group';
import { UserGroupService } from '@app/_services/user-group.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-group-list',
  templateUrl: './user-group-list.component.html',
  styleUrls: ['./user-group-list.component.less']
})
export class UserGroupListComponent implements OnInit {

  userGroups: UserGroup[];
  constructor(private userGroupService: UserGroupService, private router: Router) { }

  ngOnInit() {
    this.userGroupService.getAll().pipe().subscribe((userGroups) => {
      this.userGroups = userGroups;
    })
  }

  onSelect(userGroup){
    this.router.navigate(["userGroups", userGroup.id])
  }
}
