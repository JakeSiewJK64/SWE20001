import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { ItemsDto, ItemsListClient,ItemCategory, SalesDto, SalesItemListDto, SalesListClient, UserClient } from 'src/app/cleanarchitecture-api';
import { EditItemDetailsComponentComponent } from '../edit-item-details-component/edit-item-details-component.component';

export class CustomSalesItemDto {
  itemId: number;
  quantity: number;
}

@Component({
  selector: 'app-item-details-component',
  templateUrl: './item-details-component.component.html',
  styleUrls: ['./item-details-component.component.css']
})
export class ItemDetailsComponentComponent implements OnInit {
  constructor(private saleService: SalesListClient,
    private ItemService: ItemsListClient,
    private dialogService: MatDialog,
    private authService: AuthorizeService,
    private userService: UserClient,
    // private dialogRef: MatDialogRef<ItemDetailsComponentComponent>,
    private snackbar: MatSnackBar,
    ) 
    { this.getCreatedBy(); }

  displayedColumns: string[] = ['ItemID', 'ItemImage', 'Name', 'Type', 'Quantity'];
  dataSource: any;
  sendData: SalesDto = new SalesDto();
  ItemType = ItemCategory;
  itemList: CustomSalesItemDto[] = Array<CustomSalesItemDto>()
  customItem: CustomSalesItemDto = new CustomSalesItemDto();
  empName: string = "";

  ngOnInit() {
    this.empName.length
    this.load();
  }
  // openEditItemDetailsDialog() {
  //   this.dialogRef=this.dialogService.open(ItemDetailsComponentComponent, {
  //     width:  '250px',
  //   })
  // }

  getRemarksLength(remark: string): boolean {
    return remark.length <= 0;
  }

  load() {
    // if (this.data._salesItemList == undefined) this.data._salesItemList = new Array<SalesItemListDto>();
    // this.dataSource = this.data._salesItemList;
    // if (this.data._salesRecordId == null) {
    //   this.data._createdOn = new Date();
    //   this.data._createdBy = this.empName;
    // }
  }

  addSalesItem() {
    this.dialogService.open(EditItemDetailsComponentComponent, {
      width: '800px',
    })
  }

  getCreatedBy() {
    this.authService.getUser().subscribe(x => {
      this.empName = x.name;
    });
  }

  }
  

 

