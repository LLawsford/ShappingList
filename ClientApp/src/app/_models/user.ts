import { Role } from "./role";
import { UserGroup } from './user-group';
import { Invitation } from './invitation';
export class User {
  id: number;
  username: string;
  password: string;
  firstName: string;
  lastName: string;
  token?: string;
  role?: Role;
  group?: UserGroup;
  invitations: Invitation[];
}
