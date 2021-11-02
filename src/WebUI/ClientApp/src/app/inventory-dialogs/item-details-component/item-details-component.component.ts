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
      return;
    }
    this.model = new ItemsDto();
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
    var item = new ItemsDto();
    item._itemId = this.model.itemId;
    item._isDeleted = this.model.isDeleted;
    item._itemName = this.model.itemName;
    item._imageUrl = this.model.imageUrl;
    item._quantity = this.model.quantity;
    item._remarks = this.model.remarks;
    item._sellPrice = this.model.sellPrice;
    item._costPrice = this.model.costPrice;
    this.itemService.upsertItemsCommand(item).subscribe(x => {
      this.snackbar.open("Item Saved!", "OK", { duration: 5000 });
      this.dialogRef.close();
    })
  }
}