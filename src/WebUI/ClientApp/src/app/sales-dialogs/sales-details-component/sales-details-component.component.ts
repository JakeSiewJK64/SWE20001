import { Component, OnInit } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { EditSalesDetailsComponentComponent } from '../edit-sales-details-component/edit-sales-details-component.component';

@Component({
  selector: 'app-sales-details-component',
  templateUrl: './sales-details-component.component.html',
  styleUrls: ['./sales-details-component.component.css']
})
export class SalesDetailsComponentComponent implements OnInit {
  dialogref : any;

  constructor(private dialogservice: MatDialog) { }

  ngOnInit() {
  }

  openEditSalesDetailsDialog() {
    this.dialogref=this.dialogservice.open(EditSalesDetailsComponentComponent, {
      width:  '250px',
    })
  }

}
