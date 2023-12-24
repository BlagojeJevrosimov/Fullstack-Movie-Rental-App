import { Component, OnInit, Inject } from '@angular/core';
import { FormControl, FormGroup} from '@angular/forms';
import { Role} from '../../api/api-reference';
import {
  MAT_DIALOG_DATA,
  MatDialogRef,
} from '@angular/material/dialog';
@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrl: './customer.component.css'
})
export class CustomerComponent {
editCustomer() {
throw new Error('Method not implemented.');
}
  constructor(@Inject(MAT_DIALOG_DATA) public data: DialogData, public dialogRef: MatDialogRef<CustomerComponent>) { }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
export interface DialogData {
  customerEmail: string;
  customerRole: Role
}

