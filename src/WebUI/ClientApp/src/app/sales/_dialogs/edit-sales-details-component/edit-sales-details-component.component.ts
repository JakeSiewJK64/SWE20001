import { Component, Inject, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TdDialogService } from '@covalent/core/dialogs';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { Item, ItemsListClient, SalesItemListDto, SalesListClient } from 'src/app/cleanarchitecture-api';

@Component({
  selector: 'app-edit-sales-details-component',
  templateUrl: './edit-sales-details-component.component.html',
  styleUrls: ['./edit-sales-details-component.component.css']
})
export class EditSalesDetailsComponentComponent implements OnInit {

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: SalesItemListDto,
    private itemClient: ItemsListClient,
    private dialogService: TdDialogService,
    private dialogRef: MatDialogRef<EditSalesDetailsComponentComponent>) {
  }

  formControl = new FormControl();
  options: Observable<Item[]>;
  itemOptions: Item[] = [];
  itemQuantity: number;
  isValid: boolean = true;
  selectedItem: Item;
  errorMessage: string;

  ngOnInit() {
    this.load();
  }

  load() {
    this.getAllInventoryItems();
  }

  getAllInventoryItems() {
    this.itemClient.getAllItemsQuery().subscribe(x => x.forEach(x => this.itemOptions.push(x)));
    this.options = this.formControl.valueChanges.pipe(
      startWith(''),
      map(value => this.filterOptions(value))
    );
  }

  selectItem(item: Item) {
    this.selectedItem = new Item();
    this.selectedItem = item;
  }

  filterOptions(value: string): Item[] {
    return this.itemOptions.filter(option => option.itemName.toLowerCase().includes(value.toLowerCase()));
  }

  checkItem() {
    if (this.selectedItem == null){
      this.dialogService.openAlert({
        title: "Oops!",
        message: "The item does not exist!"
      });
      this.isValid = false;
    } else {
      this.isValid = true;
      if (this.selectedItem.quantity - this.itemQuantity < 0) {
        this.dialogService.openAlert({
          title: "Oops!",
          message: "The item is insufficient!"
        });
        this.errorMessage = "Insufficient quantity for item " + this.selectedItem.itemName;
        this.isValid = false;
      } else {
        this.isValid = true;
      }
    }
  }

  save() {
    if (this.isValid) {
      this.data.item = this.selectedItem;
      this.data.quantity = this.itemQuantity;
      this.data.itemId = this.data.item.itemId;
      this.dialogRef.close(this.data);
    }
  }

  cancel() {
    this.dialogRef.close();
  }
}
