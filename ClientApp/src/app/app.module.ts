import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";


import { AppComponent } from "./app.component";
import { appRoutingModule } from "./app.routing";

import { JwtInterceptor, ErrorInterceptor } from "./_helpers";
import { HomeComponent } from "./_components/home";
import { LoginComponent } from "./_components/login";
import { UserListComponent } from "./_components/users/user-list/user-list.component";
import { UserDetailsComponent } from "./_components/users/user-details/user-details.component";
import { RegisterComponent } from './_components/register/register.component';
import { UserGroupListComponent } from './_components/user-groups/user-group-list/user-group-list.component';
import { UserGroupDetailsComponent } from './_components/user-groups/user-group-details/user-group-details.component';
import { InvitationListComponent } from './_components/invitations/invitation-list/invitation-list.component';
import { InvitationDetailsComponent } from './_components/invitations/invitation-details/invitation-details.component';
import { InvitationNewComponent } from './_components/invitations/invitation-new/invitation-new.component';
@NgModule({
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    appRoutingModule,
  ],
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    UserListComponent,
    UserDetailsComponent,
    RegisterComponent,
    UserGroupListComponent,
    UserGroupDetailsComponent,
    InvitationListComponent,
    InvitationDetailsComponent ,
    InvitationNewComponent, ],

  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },

    // provider used to create fake backend
    //fakeBackendProvider
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
