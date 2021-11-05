import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { TdDialogService } from '@covalent/core/dialogs';
import { ExportSalesReportQuery, SalesDto, SalesItemListDto, SalesListClient } from 'src/app/cleanarchitecture-api';

@Component({
  selector: 'app-current-month-sales-report-view-dialog',
  templateUrl: './current-month-sales-report-view-dialog.component.html',
  styleUrls: ['./current-month-sales-report-view-dialog.component.css']
})
export class CurrentMonthSalesReportViewDialogComponent implements OnInit {

  dataSource: MatTableDataSource<SalesDto> = new MatTableDataSource();
  displayedColumns: string[] = ['_salesId', '_salesDate', '_remarks', '_editedOn', '_isDeleted'];
  displayedColumnsItems: string[] = ['_itemId', '_itemImage', '_itemName', '_quantity'];
  isLoading: boolean = true;
  totalRevenue: number = 0;
  itemSalesItemList = new Array<SalesItemListDto>();
  date: Date = new Date();
  itemSalesSummary: MatTableDataSource<SalesItemListDto> = new MatTableDataSource();
  constructor(private salesService: SalesListClient,
    private dialogService: TdDialogService,
    private snackbar: MatSnackBar) { }

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.getSalesCurrentMonth();
  }

  generateCSV() {
    var command: ExportSalesReportQuery = new ExportSalesReportQuery();
    command.date = new Date();
    this.salesService.generateMonthlySalesReportCommand(command).subscribe(x => {
      var url = window.URL.createObjectURL(x.data);
      var a = document.createElement('a');
      document.body.appendChild(a);
      a.setAttribute('style', 'display: none');
      a.href = url;
      a.download = x.fileName;
      a.click();
      window.URL.revokeObjectURL(url);
      a.remove();
      this.snackbar.open("Report generated successfully!", "OK", { duration: 5000 });
    }, err => {
      this.dialogService.openAlert({
        title: "Oops!",
        message: err
      })
    });
  }

  getSalesCurrentMonth() {
    var startDate = new Date();
    var endDate = new Date();
    startDate.setDate(1);
    endDate.setDate(1);
    endDate.setMonth(startDate.getMonth() + 1);
    this.salesService.getAllSalesRecordsQuery(startDate, endDate).subscribe(x => {
      x.forEach(y => {
        y._salesItemList.forEach(y => {
          this.itemSalesItemList.push(y);
        });
      });
      this.dataSource = new MatTableDataSource(x);
      var itemList = [];
      for (let i = 0; i < this.itemSalesItemList.length; i++) {
        var itemId: number = this.itemSalesItemList[i].itemId;
        var item = this.itemSalesItemList[i].item;
        var quantity: number = 0;
        if (itemList.includes(itemId)) continue;

        for (let j = 0; j < this.itemSalesItemList.length; j++) {
          if (itemId == this.itemSalesItemList[j].itemId) {
            quantity += this.itemSalesItemList[j].quantity;
          } else {
            continue;
          }
        }
        if (!itemList.includes(itemId)) {
          this.itemSalesSummary.data.push(new SalesItemListDto({
            item: item,
            quantity: quantity
          }));
        }
        this.totalRevenue += Math.round(item.sellPrice * quantity);
        itemList.push(itemId);
      }
      this.isLoading = false;
    });
  }
}
