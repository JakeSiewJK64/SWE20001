import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TdDialogService } from '@covalent/core/dialogs';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { ItemsDto, ItemsListClient, ItemCategory, Item } from 'src/app/cleanarchitecture-api';

@Component({
  selector: 'app-item-details-component',
  templateUrl: './item-details-component.component.html',
  styleUrls: ['./item-details-component.component.css']
})
export class ItemDetailsComponentComponent implements OnInit {
  constructor(private itemService: ItemsListClient,
    private authService: AuthorizeService,
    private dialogRef: MatDialogRef<ItemDetailsComponentComponent>,
    private dialogService: TdDialogService,
    private snackbar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) private data: ItemsDto) {
  }

  displayedColumns: string[] = ['ItemID', 'ItemImage', 'Name', 'Type', 'Quantity'];
  dataSource: any;
  ItemType = ItemCategory;
  model: Item = new Item();
  empName: string = "";

  ngOnInit() {
    this.load();
  }

  load() {
    this.getUser();
    if (this.data != undefined || this.data != null) {
      this.model = this.data;
      return;
    }
    this.model = new ItemsDto();
  }


  getUser() {
    this.authService.getUser().subscribe(x => {
      this.empName = x.name;
    });
  }

  uploadItemImage(files: File[]) {
    const reader = new FileReader();
    reader.readAsDataURL(files[0]);
    reader.onload = (x) => {
      this.model.imageUrl = x.target.result.toString();
    }
  }

  save(){
    var item = new ItemsDto();
    if(this.model.itemId <= 0 || this.model.itemId == null || this.model.itemId == undefined) {
      item._createdBy = this.empName;
      item._createdOn = new Date();
    }
    item._itemId = this.model.itemId;
    item._isDeleted = this.model.isDeleted;
    item._itemName = this.model.itemName;
    item._imageUrl = this.model.imageUrl;
    item._quantity = this.model.quantity;
    item._remarks = this.model.remarks;
    item._editedBy = this.empName;
    item._sellPrice = this.model.sellPrice;
    item._costPrice = this.model.costPrice;
    item._editedOn = new Date();
    item._manufacturerName = this.model.manufacturerName;
    if(item._itemName.length <= 0) {
      this.dialogService.openAlert({
        title: "Oops!",
        message: "Item name cannot be null!"
      });
      return;
    }
    this.itemService.upsertItemsCommand(item).subscribe(x => {
      this.snackbar.open("Item Saved!", "OK", { duration: 5000 });
      this.dialogRef.close();
    });
  }
}