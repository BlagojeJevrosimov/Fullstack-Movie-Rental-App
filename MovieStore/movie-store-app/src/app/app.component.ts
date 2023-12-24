import { MsalService } from '@azure/msal-angular';
import { Component } from '@angular/core';
import { Customer, CustomersClient } from './api/api-reference';
import { UserService } from './user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'MovieStore';
  loginDisplay = false;
  currentUser: Customer = new Customer();

  constructor(private authService: MsalService, private customerClient: CustomersClient, private userService: UserService) { }

  login() {
    this.authService.loginPopup()
      .subscribe({
        next: _ => {
          this.customerClient.createCustomer().subscribe({
            next: response => {
              this.userService.setUser(response);
              this.currentUser = response;
            }
          })
          this.setLoginDisplay()
        },
        error: (error) => console.log(error)
      });
  }

  logout() {
    this.authService.logoutPopup().subscribe(_ => this.loginDisplay = false);
  }

  setLoginDisplay() {
    this.loginDisplay = this.authService.instance.getAllAccounts().length > 0;
    console.log(this.authService.instance.getAllAccounts()[0].idTokenClaims)
  }
}