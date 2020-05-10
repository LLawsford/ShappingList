import { Role } from "./role";
import { User } from '.';

export class UserGroup {
  id: number;
  groupName: string;
  users: User[];
}
