import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ItemsDto, ItemsListClient, ItemCategory, Item } from 'src/app/cleanarchitecture-api';

@Component({
  selector: 'app-item-details-component',
  templateUrl: './item-details-component.component.html',
  styleUrls: ['./item-details-component.component.css']
})
export class ItemDetailsComponentComponent implements OnInit {
  constructor(private itemService: ItemsListClient,
    private dialogRef: MatDialogRef<ItemDetailsComponentComponent>,
    private snackbar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) private data: ItemsDto) {
  }

  displayedColumns: string[] = ['ItemID', 'ItemImage', 'Name', 'Type', 'Quantity'];
  dataSource: any;
  ItemType = ItemCategory;
  model: Item = new Item();

  ngOnInit() {
    this.load();
  }

  load() {
    if (this.data != undefined || this.data != null) {
      this.model = this.data;
      // this.model.itemId = this.data._itemId;
      // this.model.itemName = this.data._itemName;
      // this.model.manufacturerName = this.data._manufacturerName;
      // this.model.manufacturer_Id = this.data._manufacturer_Id;
      // this.model.costPrice = this.data._costPrice;
      // this.model.sellPrice = this.data._sellPrice;
      // this.model.status= this.data._status;
      // this.model.remarks = this.data._remarks;
      // this.model.itemCategory = this.data._itemCategory;
      // this.model.quantity = this.data._quantity;
      // this.model.batchId = this.data._batchId;
      // this.model.imageUrl = this.data._imageUrl;
      // this.model.restockDate = this.data._restockDate;
      // this.model.expDate = this.data._expDate;
      // this.model.isDeleted = this.data._isDeleted;
    }
    console.log(this.model);
  }

  uploadItemImage(files: File[]) {
    const reader = new FileReader();
    reader.readAsDataURL(files[0]);
    reader.onload = (x) => {
      this.model.imageUrl = x.target.result.toString();
    }
  }

  save(){
    console.log(this.model);
    this.itemService.upsertItemsCommand(this.model).subscribe(x => {
      console.log(x);
      this.snackbar.open("Item Saved!", "OK", { duration: 5000 });
      this.dialogRef.close();
    })
  }
}