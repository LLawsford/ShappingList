import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { environment } from "@environments/environment";
import { User } from "@app/_models";
import { Invitation } from '@app/_models/invitation';

@Injectable({ providedIn: "root" })
export class UserService {
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<User[]>(`${environment.apiUrl}/users`);
  }

  getById(id: number) {
    return this.http.get<User>(`${environment.apiUrl}/users/${id}`);
  }

  register(user: User) {
    return this.http.post(`${environment.apiUrl}/users/register`, user);
  }

  showAllInvitations(userId: number) {
    return this.http.get<Invitation[]>(`${environment.apiUrl}/users/${userId}/invitations`);
  }

  acceptInvitation(userId: number, invitationId: number) {
    return this.http.post(`${environment.apiUrl}/users/${userId}/invitations/${invitationId}/accept`, null);
  }

  declineInvitation(userId: number, invitationId: number) {
    return this.http.post(`${environment.apiUrl}/users/${userId}/invitations/${invitationId}/decline`, null);
  }
}
