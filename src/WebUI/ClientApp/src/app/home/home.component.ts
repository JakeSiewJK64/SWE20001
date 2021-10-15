import {Component, ViewChild} from '@angular/core';
import {MatTable} from '@angular/material/table';
import {EditSalesDialogComponent} from '../_dialog/edit-sales-dialog/edit-sales-dialog.component';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
export interface SalesRecord {
  Sales_ID : number;
  Date: number;
  Employee_ID: number;
  Item_ID: number;
  Quantity : number
  Remarks: string;
  isDeleted: boolean;
}

const ELEMENT_DATA: SalesRecord[] = [
  {Sales_ID: 1, Date: 1, Employee_ID: 123, Item_ID: 321, Quantity: 666, Remarks:'remarks', isDeleted:true},
  {Sales_ID: 2, Date: 1, Employee_ID: 123, Item_ID: 321, Quantity: 666, Remarks:'remarks', isDeleted:true},
  {Sales_ID: 3, Date: 1, Employee_ID: 123, Item_ID: 321, Quantity: 666, Remarks:'remarks', isDeleted:true},
  {Sales_ID: 4, Date: 1, Employee_ID: 123, Item_ID: 321, Quantity: 666, Remarks:'remarks', isDeleted:true},
  {Sales_ID: 5, Date: 1, Employee_ID: 123, Item_ID: 321, Quantity: 666, Remarks:'remarks', isDeleted:true},
  {Sales_ID: 6, Date: 1, Employee_ID: 123, Item_ID: 321, Quantity: 666, Remarks:'remarks', isDeleted:true},
  {Sales_ID: 7, Date: 1, Employee_ID: 123, Item_ID: 321, Quantity: 666, Remarks:'remarks', isDeleted:true},
  {Sales_ID: 8, Date: 1, Employee_ID: 123, Item_ID: 321, Quantity: 666, Remarks:'remarks', isDeleted:true},
  {Sales_ID: 9, Date: 1, Employee_ID: 123, Item_ID: 321, Quantity: 666, Remarks:'remarks', isDeleted:true},
  {Sales_ID: 10, Date: 1, Employee_ID: 123, Item_ID: 321, Quantity: 666, Remarks:'remarks', isDeleted:true},
];

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  displayedColumns: string[] = ['Sales_ID', 'Date', 'Employee_ID', 'Item_ID', 'Quantity', 'Remarks', 'isDeleted'];
  dataSource = ELEMENT_DATA;
  dialogref : any;

  @ViewChild(MatTable) table: MatTable<SalesRecord>;

  constructor(private dialogservice: MatDialog){

  }

  addData() {
    const randomElementIndex = Math.floor(Math.random() * ELEMENT_DATA.length);
    this.dataSource.push(ELEMENT_DATA[randomElementIndex]);
    this.table.renderRows();
  }

  openeditdialog() {
    this.dialogref=this.dialogservice.open(EditSalesDialogComponent, {
      width:  '250px',
    })
  }



}
