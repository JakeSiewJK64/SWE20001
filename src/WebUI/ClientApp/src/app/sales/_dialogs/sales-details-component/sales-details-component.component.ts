import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Item, ItemCategory, SalesDto, SalesItemListDto, SalesListClient } from 'src/app/cleanarchitecture-api';
import { EditSalesDetailsComponentComponent } from '../edit-sales-details-component/edit-sales-details-component.component';

export class CustomSalesItemDto {
  itemId: number;
  quantity: number;
}
@Component({
  selector: 'app-sales-details-component',
  templateUrl: './sales-details-component.component.html',
  styleUrls: ['./sales-details-component.component.css']
})
export class SalesDetailsComponentComponent implements OnInit {

  constructor(private saleService: SalesListClient,
    private dialogservice: MatDialog,
    private userClient: AuthorizeService,
    private dialogRef: MatDialogRef<SalesDetailsComponentComponent>,
    @Inject(MAT_DIALOG_DATA) public data: SalesDto) { }

  displayedColumns: string[] = ['ItemID', 'Name', 'Type', 'Quantity'];
  salesDate: Date = new Date(this.data._date);
  dataSource: any;
  sendData: SalesDto = new SalesDto();
  ItemType = ItemCategory;
  itemList: CustomSalesItemDto[] = Array<CustomSalesItemDto>()
  customItem: CustomSalesItemDto = new CustomSalesItemDto();

  ngOnInit() {
    this.load();
  }

  load() {
    this.dataSource = this.data._salesItemList;
  }

  saveSales() {
    this.data._salesItemList.forEach(x => {
      this.customItem = new CustomSalesItemDto()
      this.customItem.itemId = x.itemId
      this.customItem.quantity = x.quantity
      this.itemList.push(this.customItem)
    })
    this.data._items = JSON.stringify(this.itemList).replace('"', '\"');

    this.sendData._salesItemList = [];
    this.sendData.lastModifiedBy = "jake";
    this.sendData._remarks = this.data._remarks;
    this.sendData._date = this.salesDate.toString();
    this.sendData._employeeId = this.data._employeeId;
    this.sendData._items = this.data._items;
    this.sendData._salesRecordId = this.data._salesRecordId;
    this.saleService.upsertSalesCommand(this.sendData).subscribe(x => {
      console.log(x);
      this.dialogRef.close();
    });
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
