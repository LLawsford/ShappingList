import { Routes, RouterModule } from "@angular/router";

import { HomeComponent } from "./_components/home/home.component";
import { LoginComponent } from "./_components/login/login.component";
import { AuthGuard } from "./_helpers";
import { UserListComponent } from "./_components/users/user-list/user-list.component";
import { UserDetailsComponent } from "./_components/users/user-details/user-details.component";
import { RegisterComponent } from './_components/register/register.component';
import { UserGroupListComponent } from './_components/user-groups/user-group-list/user-group-list.component';
import { UserGroupDetailsComponent } from './_components/user-groups/user-group-details/user-group-details.component';
import { InvitationListComponent } from './_components/invitations/invitation-list/invitation-list.component';

const routes: Routes = [
  { path: "", component: HomeComponent, canActivate: [AuthGuard] },
  { path: "login", component: LoginComponent },
  { path: "register", component: RegisterComponent  },
  { path: "users", component: UserListComponent, canActivate: [AuthGuard] },
  { path: "users/:id", component: UserDetailsComponent, canActivate: [AuthGuard]},

  { path: "userGroups", component: UserGroupListComponent, canActivate: [AuthGuard]  },
  { path: "userGroups/:id", component: UserGroupDetailsComponent, canActivate: [AuthGuard]  },

  { path: "users/:userId/invitations", component: InvitationListComponent, canActivate: [AuthGuard]  },


  // otherwise redirect to home
  { path: "**", redirectTo: "" },
];

export const appRoutingModule = RouterModule.forRoot(routes);
