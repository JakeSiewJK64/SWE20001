import { Component, OnInit } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { EditItemDetailsComponentComponent } from '../edit-item-details-component/edit-item-details-component.component';

@Component({
  selector: 'app-item-details-component',
  templateUrl: './item-details-component.component.html',
  styleUrls: ['./item-details-component.component.css']
})
export class ItemDetailsComponentComponent implements OnInit {
  dialogref : any;

  constructor(private dialogservice: MatDialog) { }

  ngOnInit() {
  }

  openEditItemDetailsDialog() {
    this.dialogref=this.dialogservice.open(EditItemDetailsComponentComponent, {
      width:  '250px',
    })
  }

}
