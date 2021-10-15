import { Component, OnInit } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { ItemDetailsComponentComponent } from '../inventory-dialogs/item-details-component/item-details-component.component';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {
  dialogref : any;

  constructor(private dialogservice: MatDialog) { }

  ngOnInit() {
  }

  openItemDetailsDialog() {
    this.dialogref=this.dialogservice.open(ItemDetailsComponentComponent, {
      width:  '250px',
    })
  }

}
