import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTable } from '@angular/material/table';
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
    private authService: AuthorizeService,
    private userService: UserClient,
    private dialogRef: MatDialogRef<SalesDetailsComponentComponent>,
    private dialogService: TdDialogService,
    private snackbar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) public data: SalesDto) { this.getCreatedBy(); }

  @ViewChild(MatTable) private table: MatTable<SalesItemListDto>;
  displayedColumns: string[] = ['ItemID', 'ItemImage', 'Name', 'Type', 'Quantity', 'Remove'];
  salesDate: Date = new Date(this.data._salesDate);
  dataSource = [];
  sendData: SalesDto = new SalesDto();
  ItemType = ItemCategory;
  itemList: CustomSalesItemDto[] = Array<CustomSalesItemDto>()
  customItem: CustomSalesItemDto = new CustomSalesItemDto();
  empName: string = "";

  ngOnInit() {
    this.empName.length
    this.load();
  }

  removeItem(i: number) {
    this.dataSource.splice(i, 1);
    this.table.renderRows();
  }

  getRemarksLength(remark: string): boolean {
    return remark.length <= 0;
  }

  load() {
    if (this.data._salesRecordId == null) {
      this.data._createdOn = new Date();
      this.salesDate = this.data._createdOn;
      this.authService.getUser().subscribe(x => {
        if(x == null) return;
        this.data._createdBy = x.name
      });
      this.data._salesItemList = new Array<SalesItemListDto>();
    }
    this.dataSource = this.data._salesItemList;
  }

  addSalesItem() {
    this.dialogService.open(EditSalesDetailsComponentComponent, {
      width: '800px',
      data: new SalesItemListDto()
    }).afterClosed().subscribe(x => {
      if (x == null || x == undefined) return;
      this.dataSource.push(x);
      this.table.renderRows();
      this.snackbar.open("Item added successfully!", "OK", { duration: 5000 });
    })
  }

  getCreatedBy() {
    this.authService.getUser().subscribe(x => {
      if (x == null) return;
      this.empName = x.name;
    });
  }

  saveSales() {
    if (this.data._salesItemList.length < 1) {
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
    this.sendData._remarks = this.data._remarks;
    this.sendData._salesDate = this.salesDate;
    this.sendData._employeeId = this.data._employeeId;
    this.sendData._createdBy = this.data._createdBy;
    this.sendData._editedBy = this.empName;
    this.sendData._items = this.data._items;
    this.sendData._salesRecordId = this.data._salesRecordId;
    this.sendData._editedOn = new Date();
    this.sendData._isDeleted = this.data._isDeleted;
    this.saleService.upsertSalesCommand(this.sendData).subscribe(x => {
      console.log(x);
      if (this.data._isDeleted) {
        this.snackbar.open("Sales deleted successfully!", "OK", { duration: 5000 });
        return;
      }
      this.snackbar.open("Sales Details Saved!", "OK", { duration: 5000 });
      this.dialogRef.close();
    }, err => {
      this.dialogService.openAlert({
        title: "Oops!",
        message: err
      });
    });
  }

  deleteSales() {
    this.dialogService.openConfirm({
      title: "Delete Sales",
      message: "Are you sure you want to delete sales record " + this.data._salesRecordId + "?",
      acceptButton: "Ok",
      cancelButton: "Cancel"
    }).afterClosed().subscribe(x => {
      if (x) {
        this.data._isDeleted = true;
        this.saveSales();
      }
    });
  }

  deleteSales(){
    
  }

  closeDialog() {
    this.dialogRef.close();
  }
}
