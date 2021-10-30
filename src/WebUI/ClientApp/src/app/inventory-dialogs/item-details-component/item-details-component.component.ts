import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ItemsDto, ItemsListClient, ItemCategory } from 'src/app/cleanarchitecture-api';

@Component({
  selector: 'app-item-details-component',
  templateUrl: './item-details-component.component.html',
  styleUrls: ['./item-details-component.component.css']
})
export class ItemDetailsComponentComponent implements OnInit {
  constructor(private itemService: ItemsListClient,
    private dialogRef: MatDialogRef<ItemDetailsComponentComponent>,
    private snackbar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) public data: ItemsDto) {
  }

  displayedColumns: string[] = ['ItemID', 'ItemImage', 'Name', 'Type', 'Quantity'];
  dataSource: any;
  ItemType = ItemCategory;
  model: ItemsDto = new ItemsDto();

  ngOnInit() {
    this.load();
  }

  load() {
    if (this.data != undefined || this.data != null) this.model = this.data;
    console.log(this.model);
  }

  uploadItemImage(files: File[]) {
    const reader = new FileReader();
    reader.readAsDataURL(files[0]);
    reader.onload = (x) => {
      this.model._imageUrl = x.target.result.toString();
    }
  }

  save() {
    this.model._itemCategory;
    this.model._quantity;
    this.model._batchId;
    this.model._manufacturer_Id;
    this.model._costPrice;
    this.model._sellPrice;
    this.model._itemName;
    this.model._imageUrl;
    this.model._manufacturerName;
    this.model._remarks;
    this.model._restockDate;
    this.model._expDate;
    this.model._isDeleted;
    this.itemService.upsertItemsCommand(this.model).subscribe(x => {
      console.log(x);
      this.snackbar.open("Item Saved!", "OK", { duration: 5000 });
      this.dialogRef.close();
    })
  }
}