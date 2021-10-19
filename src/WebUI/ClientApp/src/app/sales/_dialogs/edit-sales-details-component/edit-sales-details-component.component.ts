import { Component, Inject, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { SalesItemListDto, SalesListClient } from 'src/app/cleanarchitecture-api';

@Component({
  selector: 'app-edit-sales-details-component',
  templateUrl: './edit-sales-details-component.component.html',
  styleUrls: ['./edit-sales-details-component.component.css']
})
export class EditSalesDetailsComponentComponent implements OnInit {

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: SalesItemListDto,
    private salesClient: SalesListClient,
    private dialogRef: MatDialogRef<EditSalesDetailsComponentComponent>) { }
  formControl = new FormControl();
  options: Observable<string[]>;
  itemOptions = ["Acetaminophen (Tylenol)", "Aspirin", "naproxen", "ibuprofen", "Folic Acid", "Iron Supplements", "Alprazolam"];

  ngOnInit() {
    this.load();
  }

  load() {
    this.getAllInventoryItems("");
  }

  getAllInventoryItems(searchCriteria: string) {
    this.options = this.formControl.valueChanges.pipe(
      startWith(''),
      map(value => this.filterOptions(value))
    );
  }

  filterOptions(value: string): string[] {
    return this.itemOptions.filter(option => option.toLowerCase().includes(value.toLowerCase()))
  }

  save() {
  }

  cancel() {
    this.dialogRef.close();
  }
}
