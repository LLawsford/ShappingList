import { Routes, RouterModule } from "@angular/router";

import { HomeComponent } from "./_components/home/home.component";
import { LoginComponent } from "./_components/login/login.component";
import { AuthGuard } from "./_helpers";
import { UserListComponent } from "./_components/users/user-list/user-list.component";
import { UserDetailsComponent } from "./_components/users/user-details/user-details.component";

const routes: Routes = [
  { path: "", component: HomeComponent, canActivate: [AuthGuard] },
  { path: "login", component: LoginComponent },
  { path: "users", component: UserListComponent, canActivate: [AuthGuard] },
  { path: "users/:id", component: UserDetailsComponent },
  // otherwise redirect to home
  { path: "**", redirectTo: "" },
];

export const appRoutingModule = RouterModule.forRoot(routes);
