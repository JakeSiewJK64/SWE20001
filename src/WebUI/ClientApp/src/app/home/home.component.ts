import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTable } from '@angular/material/table';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SalesDto, SalesListClient } from '../cleanarchitecture-api';
import { SalesDetailsComponentComponent } from '../sales/_dialogs/sales-details-component/sales-details-component.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  displayedColumns: string[] = ['Sales_ID', 'Date', 'Employee_ID', 'Remarks', 'isDeleted', 'CreatedBy', 'LastModifiedBy'];
  dataSource: any;
  dialogref: any;
  isLoading: boolean = false;
  date: Date = new Date();

  @ViewChild(MatTable) table: MatTable<SalesDto>;

  constructor(private salesService: SalesListClient, private dialogservice: MatDialog) {
  }

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.isLoading = true;
    this.salesService.getAllSalesRecordsQuery().subscribe(x => this.dataSource = x);
    this.isLoading = false;
  }

  addData() {
  }

  openEditDialog(data: any) {
    this.dialogref = this.dialogservice.open(SalesDetailsComponentComponent, {
      width: '1000px',
      data: data
    })
  }

  openEditDialogNew() {
    this.dialogref = this.dialogservice.open(SalesDetailsComponentComponent, {
      width: '1000px',
      data: new SalesDto()
    })
  }
}
