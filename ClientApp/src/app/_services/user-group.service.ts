import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { environment } from "@environments/environment";
import { UserGroup } from '@app/_models/user-group';
import { Observable } from 'rxjs';

@Injectable({ providedIn: "root" })
export class UserGroupService {
  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<UserGroup[]>(`${environment.apiUrl}/userGroups`);
  }

  getById(id: number) {
    return this.http.get<UserGroup>(`${environment.apiUrl}/userGroups/${id}`);
  }

  inviteUser(userGroupId: number, userId: number){
    return this.http.post(`${environment.apiUrl}/userGroups/${userGroupId}/users/${userId}`, null);
  }

}
