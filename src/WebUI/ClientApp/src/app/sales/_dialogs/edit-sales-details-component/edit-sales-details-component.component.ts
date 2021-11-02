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
    private salesClient: SalesListClient,
    private itemClient: ItemsListClient,
    private dialogService: TdDialogService,
    private dialogRef: MatDialogRef<EditSalesDetailsComponentComponent>) {
  }

  formControl = new FormControl();
  options: Observable<Item[]>;
  itemOptions: Item[] = [];
  itemQuantity: number;
  selectedItem: Item = new Item();

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
    this.selectedItem = item;
  }

  filterOptions(value: string): Item[] {
    console.log(value);
    return this.itemOptions.filter(option => option.itemName.toLowerCase().includes(value.toLowerCase()));
  }

  save() {
    this.data.item = this.selectedItem;
    this.data.quantity = this.itemQuantity;
    this.data.itemId = this.data.item.itemId;
    this.dialogRef.close(this.data);
  }

  cancel() {
    this.dialogRef.close();
  }
}
