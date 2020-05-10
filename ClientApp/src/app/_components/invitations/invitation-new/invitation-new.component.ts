import { Component, OnInit, Input } from '@angular/core';
import { UserGroup } from '@app/_models/user-group';
import { UserGroupService } from '@app/_services/user-group.service';
import { pipe } from 'rxjs';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { parse } from 'querystring';
import { AuthenticationService } from '@app/_services';

@Component({
  selector: 'app-invitation-new',
  templateUrl: './invitation-new.component.html',
  styleUrls: ['./invitation-new.component.less']
})
export class InvitationNewComponent implements OnInit {
  @Input() userGroupId: string;
  private userGroup: UserGroup;
  private invitationForm: FormGroup;
  private error: string = '';
  private submitted: boolean = false;

  constructor(private userGroupService: UserGroupService, private formBuilder: FormBuilder, private router: Router, private authenticationService: AuthenticationService) { }

  ngOnInit() {

    this.invitationForm = this.formBuilder.group({
      userId: ['', Validators.required]});

    this.userGroupService.getById(parseInt(this.userGroupId)).pipe().subscribe(userGroup => this.userGroup = userGroup);
  }

  get f() { return this.invitationForm.controls; }

  onSubmit() {
    this.submitted = true;

    if (this.invitationForm.invalid) {
      console.log("form invalid");  
      return;
        
    }

    this.userGroupService.inviteUser(parseInt(this.userGroupId), parseInt(this.f.userId.value));
}

}
