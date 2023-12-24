import { NgModule } from "@angular/core";
import { MatButtonModule } from "@angular/material/button";
import { MatMenuModule } from "@angular/material/menu";
import { MatTableModule } from "@angular/material/table";
import { CustomerTableComponent } from "./customer-table/customer-table.component";
import { CommonModule } from "@angular/common";
import { CustomersRoutingModule } from "./customers-routing.module";
import { CustomerComponent } from "./customer/customer.component";
import { ReactiveFormsModule } from "@angular/forms";
import { MatSelectModule } from "@angular/material/select";
import { MatInputModule } from "@angular/material/input";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatCardModule } from "@angular/material/card";
import { MatDialogModule } from '@angular/material/dialog';
import { FormsModule } from "@angular/forms";

@NgModule({
    imports: [
        MatTableModule,
        MatButtonModule,
        MatMenuModule,
        CommonModule,
        CustomersRoutingModule,
        ReactiveFormsModule,
        MatSelectModule,
        MatInputModule,
        MatFormFieldModule,
        MatCardModule,
        MatButtonModule,
        MatDialogModule, 
        FormsModule

    ],
    declarations: [CustomerTableComponent, CustomerComponent],
})

export class CustomersModule { }