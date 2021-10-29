import { MatDialog } from '@angular/material/dialog';
import { ItemDetailsComponentComponent } from '../inventory-dialogs/item-details-component/item-details-component.component';
import { ItemCategory } from 'src/app/cleanarchitecture-api';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ItemsDto, ItemsListClient } from '../cleanarchitecture-api';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

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
  page: number;
  pageSize: number;
  totalRecord: number;

  @ViewChild(MatTable) table: MatTable<ItemsDto>;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private dialogservice: MatDialog, 
    private itemService: ItemsListClient) {
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
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      this.totalRecord = x.length;
      this.isLoading = false;
    });
  }

  filterItems() {
  }

  onPageChanged(pageEvent: any) {
    this.page = pageEvent.pageIndex + 1;
    this.pageSize = pageEvent.pageSize;
    this.load();
  }

  openAddItemDialog() {
    this.dialogref = this.dialogservice.open(ItemDetailsComponentComponent, {
      width: '1000px',
      data: new ItemsDto()
    })
  }
}