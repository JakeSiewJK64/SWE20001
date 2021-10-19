import { Component, OnInit } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { SalesDetailsComponentComponent } from './_dialogs/sales-details-component/sales-details-component.component';

@Component({
  selector: 'app-sales',
  templateUrl: './sales.component.html',
  styleUrls: ['./sales.component.css']
})
export class SalesComponent implements OnInit {
  dialogref : any;

  constructor(private dialogservice: MatDialog) { }

  ngOnInit() {
  }

  openSalesDetailsDialog() {
    this.dialogref=this.dialogservice.open(SalesDetailsComponentComponent, {
      width:  '250px',
    })
  }

}
