import { UserGroup } from './user-group';
import {User} from './user';
export class Invitation {
  id: number;
  group: UserGroup;
  user: User;
  isAccepted: boolean;
}
