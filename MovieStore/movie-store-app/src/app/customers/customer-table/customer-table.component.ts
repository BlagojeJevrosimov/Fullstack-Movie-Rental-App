import { Component } from '@angular/core';
import { Customer, CustomerStatus, CustomersClient, DeleteCustomerByIdCommand, ExpirationDate, PromoteCustomerCommand, Role, Status, UpdateCustomerCommand } from '../../api/api-reference';
import { MatDialog } from '@angular/material/dialog';
import { CustomerComponent } from '../customer/customer.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-customer-table',
  templateUrl: './customer-table.component.html',
  styleUrl: './customer-table.component.css'
})
export class CustomerTableComponent {
  displayedColumns: string[] = ['email', 'status', 'statusExpirationDate', 'role', 'action']
  customers: Customer[] = [];
  statusEnum: typeof Status = Status;
  roleEnum: typeof Role = Role;
  constructor(private readonly customersClient: CustomersClient, private dialog: MatDialog, private readonly snackbar: MatSnackBar) {
  }
  ngOnInit() {
    this.customersClient.getAllCustomers().subscribe(response => this.customers = response);
  }
  deleteCustomer(customerId: string) {
    this.customersClient.deleteCustomerById(new DeleteCustomerByIdCommand({
      id: customerId
    })).subscribe(_ => this.customers = this.customers.filter(customer => customer.id !== customerId))
  }
  openDialog(customer: Customer): void {
    const dialogRef = this.dialog.open(CustomerComponent, {
      data: {
        customerEmail: customer.email,
        customerRole: customer.role
      },
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        this.customersClient.updateCustomer(new UpdateCustomerCommand({
          id: customer.id,
          email: result.customerEmail,
          role: result.customerRole === "0" ? Role.Regular : Role.Administrator
        })).subscribe(_ => {
          let cust = this.customers.at(this.customers.indexOf(customer))
          cust!.email = result.customerEmail
          cust!.role = result.customerRole === "0" ? Role.Regular : Role.Administrator
        })
      }
    });
  }
  promoteCustomer(customer: Customer) {
    this.customersClient.promote(new PromoteCustomerCommand(
      {
        customerId: customer.id
      }
    )).subscribe({
      next: Response => this.customers[this.customers.indexOf(customer)] = Response,
      error: err => this.snackbar.open("Customer not eligible for promotion")
    })
  }
}
