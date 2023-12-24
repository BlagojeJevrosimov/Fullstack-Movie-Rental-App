// src/app/auth/role-guard.service.ts
import { Injectable } from '@angular/core';
import { CanActivate} from '@angular/router';
import { UserService } from '../user.service';
import { Role } from '../api/api-reference';


@Injectable()
export class AdminRoleGuard implements CanActivate {
  constructor(private userService: UserService) {}
  canActivate(): boolean {

    if (this.userService.getUser().role != Role.Administrator){
      return false
    }
     return true;
  }
}