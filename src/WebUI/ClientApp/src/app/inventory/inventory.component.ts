import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ItemDetailsComponentComponent } from '../inventory-dialogs/item-details-component/item-details-component.component';
import { AuditableEntity, Item, ItemCategory } from 'src/app/cleanarchitecture-api';
import { MatTableDataSource } from '@angular/material/table';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatTable } from '@angular/material/table';
import { ItemsDto, ItemsListClient } from '../cleanarchitecture-api';
import { SalesDetailsComponentComponent } from '../sales/_dialogs/sales-details-component/sales-details-component.component';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {
  displayedColumns: string[] = ['itemId', 'itemName', 'itemCategory', 'quantity'];
  ItemType = ItemCategory;
  dataSource: MatTableDataSource<ItemsDto>;
  dialogref: any;
  isLoading: boolean = false;

  constructor(private dialogservice: MatDialog, private itemService: ItemsListClient) {
  }

  ngOnInit() {
    this.load();
  }

  load() {
    this.isLoading = true;
    this.getItem();
    this.dataSource = new MatTableDataSource<ItemsDto>();
  }

  getItem() {
    this.itemService.getAllItemsQuery().subscribe(x => {
      this.dataSource = new MatTableDataSource(x);
      this.isLoading = false;
    });
  }

  openAddItemDialog() {
    this.dialogref = this.dialogservice.open(ItemDetailsComponentComponent, {
      width: '1000px',
      data: new ItemsDto()
    })
  }
}