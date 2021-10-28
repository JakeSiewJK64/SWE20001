import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { ItemsDto, ItemsListClient, ItemCategory, SalesDto, SalesItemListDto, SalesListClient, UserClient } from 'src/app/cleanarchitecture-api';
import { EditItemDetailsComponentComponent } from '../edit-item-details-component/edit-item-details-component.component';

@Component({
  selector: 'app-item-details-component',
  templateUrl: './item-details-component.component.html',
  styleUrls: ['./item-details-component.component.css']
})
export class ItemDetailsComponentComponent implements OnInit {
  constructor(private saleService: SalesListClient,
    private ItemService: ItemsListClient,
    // private dialogService: MatDialog,
    private authService: AuthorizeService,
    private userService: UserClient,
    // private dialogRef: MatDialogRef<ItemDetailsComponentComponent>,
    private snackbar: MatSnackBar,
  ) {
  }

  displayedColumns: string[] = ['ItemID', 'ItemImage', 'Name', 'Type', 'Quantity'];
  dataSource: any;
  ItemType = ItemCategory;
  empName: string = "";
  model: ItemsDto = new ItemsDto();

  ngOnInit() {
    this.empName.length
    this.load();
  }

  load() {
  }

  uploadItemImage(files: File[]) {
    console.log(files)
    const reader = new FileReader();
    reader.readAsDataURL(files[0]);
    reader.onload = (x) => {
      this.model._imageUrl = x.target.result.toString();
    }
  }

  save() {
    this.ItemService.upsertItemsCommand(this.model).subscribe(x => {
      console.log(x);
    })
  }
}