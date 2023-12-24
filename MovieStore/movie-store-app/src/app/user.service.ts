import { Injectable } from '@angular/core';
import { Customer } from './api/api-reference';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private currentUser: Customer = new Customer

  setUser(user: Customer) {
    this.currentUser = user;
  }

  getUser() {
    return this.currentUser;
  }
}
