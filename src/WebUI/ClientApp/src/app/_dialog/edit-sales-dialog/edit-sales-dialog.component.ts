import { Component, OnInit } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';


@Component({
  selector: 'app-edit-sales-dialog',
  templateUrl: './edit-sales-dialog.component.html',
  styleUrls: ['./edit-sales-dialog.component.css']
})
export class EditSalesDialogComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<EditSalesDialogComponent>
   ) {}

  ngOnInit(): void {
  }

}
