import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TdDialogService } from '@covalent/core/dialogs';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Item, ItemCategory, SalesDto, SalesItemListDto, SalesListClient, UserClient } from 'src/app/cleanarchitecture-api';
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
    private dialogService: MatDialog,
    private userService: UserClient,
    private dialogRef: MatDialogRef<SalesDetailsComponentComponent>,
    private snackbar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) public data: SalesDto) { }

  displayedColumns: string[] = ['ItemID', 'Name', 'Type', 'Quantity'];
  salesDate: Date = new Date(this.data._date);
  dataSource: any;
  sendData: SalesDto = new SalesDto();
  ItemType = ItemCategory;
  itemList: CustomSalesItemDto[] = Array<CustomSalesItemDto>()
  customItem: CustomSalesItemDto = new CustomSalesItemDto();
  empName: string = "";

  ngOnInit() {
    this.load();
  }

  load() {
    if(this.data._salesItemList == undefined) this.data._salesItemList = new Array<SalesItemListDto>();
    this.getUserById(this.data._employeeId);
    this.dataSource = this.data._salesItemList;
  }

  addSalesItem() {
    this.dialogService.open(EditSalesDetailsComponentComponent, {
      width: '800px',
    })
  }

  getUserById(id: string){
    var returnName;
    this.userService.getUserByIdQuery(id).subscribe(x => this.empName = x.userName);
  }

  saveSales() {
    if(this.data._salesItemList.length < 1)  {
      this.snackbar.open("Sales Items cannot be empty!", "OK", { duration: 5000 });
      return;
    }

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
}
