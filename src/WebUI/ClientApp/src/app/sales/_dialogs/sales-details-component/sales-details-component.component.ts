import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ItemCategory, SalesDto, SalesListClient } from 'src/app/cleanarchitecture-api';
import { EditSalesDetailsComponentComponent } from '../edit-sales-details-component/edit-sales-details-component.component';

@Component({
  selector: 'app-sales-details-component',
  templateUrl: './sales-details-component.component.html',
  styleUrls: ['./sales-details-component.component.css']
})
export class SalesDetailsComponentComponent implements OnInit {

  constructor(private saleService: SalesListClient,
    private dialogservice: MatDialog, 
    private dialogRef: MatDialogRef<SalesDetailsComponentComponent>,
    @Inject(MAT_DIALOG_DATA) public data: SalesDto) { }
    displayedColumns: string[] = ['ItemID', 'Name', 'Type', 'Quantity'];
  salesDate: Date = new Date(this.data._date);
  dataSource: any;
  ItemType = ItemCategory;
  ngOnInit() {
    this.load();
  }

  load() {
    this.dataSource = this.data._salesItemList;
  }

  saveSales() {
    this.data._date = this.salesDate.toString();
    this.saleService.upsertSalesCommand(this.data).subscribe(x => console.log(x));
  }

  closeDialog() {
    this.dialogRef.close();
  }

  // openEditSalesDetailsDialog() {
  //   this.dialogref = this.dialogservice.open(EditSalesDetailsComponentComponent, {
  //     width: '250px',
  //   })
  // }

}
