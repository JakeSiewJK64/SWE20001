import { SalesDetailsComponentComponent } from './_dialogs/sales-details-component/sales-details-component.component';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { ExportSalesReportQuery, Item, ItemCategory, ItemsListClient, SalesDto, SalesListClient } from '../cleanarchitecture-api';
import { TdDialogService } from '@covalent/core/dialogs';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { CurrentMonthSalesReportViewDialogComponent } from './_dialogs/current-month-sales-report-view-dialog/current-month-sales-report-view-dialog.component';
@Component({
  selector: 'app-sales',
  templateUrl: './sales.component.html',
  styleUrls: ['./sales.component.css']
})
export class SalesComponent implements OnInit {
  dialogref: any;
  displayedColumns: string[] = ['_salesRecordId', '_salesDate', '_remarks', '_editedOn', '_isDeleted'];
  dataSource: MatTableDataSource<SalesDto>;
  isLoading: boolean = false;
  filterCriteria: string;
  itemList: Item[] = [];
  itemCategoryLists = ItemCategory;
  date = new Date();
  startDate: Date;
  endDate: Date;
  page: number;
  pageSize: number;
  totalRecord: number;
  productIdFilter: number;
  productCategoryFilter: string;
  predictedProductSales: number;
  predictedItemCategorySales: number;
  isAdmin: boolean = false;

  highestSellingItem: string;
  highestSellingItemCategory: string;
  totalSalesCurrentMonth: number;

  @ViewChild(MatTable) table: MatTable<SalesDto>;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(private salesService: SalesListClient,
    private snackbar: MatSnackBar,
    private itemService: ItemsListClient,
    private authService: AuthorizeService,
    private tdDialogService: TdDialogService,
    private dialogservice: MatDialog) {
  }

  ngOnInit(): void {
    this.startDate = new Date(this.date.getFullYear(), this.date.getMonth(), 1);
    this.endDate = new Date(this.date.getFullYear(), this.date.getMonth() + 1, 0)
    this.load();
  }

  load() {
    if(this.startDate != null && this.endDate != null) this.isLoading = true;
    this.getItems();
    this.dataSource = new MatTableDataSource<SalesDto>();
    this.getSales();
    this.getUser();
    this.getTotalSalesCurrentMonth();
    this.getHighestSellingItem();
    this.getHighestSellingItemCategory();
  }

  getUser() {
    this.authService.getUser().subscribe(x => {
      this.isAdmin = x.role == 'Administrator';
    });
  }

  openViewCurrentMonthSalesDialog() {
    this.dialogservice.open(CurrentMonthSalesReportViewDialogComponent, {
      maxHeight: '800px',
      height: '800px',
      width: '1000px',
      maxWidth: '1000px',
    });
  }

  getSales(){
    this.salesService.getAllSalesRecordsQuery(this.startDate, this.endDate).subscribe(x => {
      this.dataSource = new MatTableDataSource(x);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      this.totalRecord = x.length;
      this.isLoading = false;
    });
  }

  getTotalSalesCurrentMonth() {
    this.salesService.getTotalSalesForCurrentMonth(new Date()).subscribe(x => this.totalSalesCurrentMonth = x);
  }

  getHighestSellingItem() {
    this.salesService.getHighestSellingItemQuery(new Date()).subscribe(x => this.highestSellingItem = x);
  }

  getHighestSellingItemCategory() {
    this.salesService.getHighestSellingItemCategoryQuery(new Date()).subscribe(x => this.highestSellingItemCategory = x);
  }

  predictProductSales() {
    this.salesService.predictSalesOfItemForNextMonthQuery(this.productIdFilter, new Date()).subscribe(x => {
      this.predictedProductSales = x;
    })
  }

  predictItemTypeSales() {
    this.salesService.predictSalesByItemTypeQuery(this.productCategoryFilter).subscribe(x => {
      this.predictedItemCategorySales = x;
    })
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
      this.tdDialogService.openAlert({
        title: "Oops!",
        message: err
      })
    });
  }

  filterSales() {

    if (this.filterCriteria.length < 1) {
      this.load();
      return;
    }

    this.salesService.getSalesBySearchCriteriaQuery(this.filterCriteria).subscribe(x => {
      if (x.length < 1) {
        this.tdDialogService.openAlert({
          title: 'Oops!',
          message: 'The sales record you are trying to find does not exist!',
          closeButton: 'Close'
        })
        this.load();
        return;
      }
      this.dataSource = new MatTableDataSource(x);
      this.isLoading = false;
    });
    this.isLoading = false;
  }

  getItems() {
    this.itemService.getAllItemsQuery().subscribe(x => {
      this.itemList = x;
    });
  }

  onPageChanged(pageEvent: any) {
    this.page = pageEvent.pageIndex + 1;
    this.pageSize = pageEvent.pageSize;
    this.load();
  }

  openEditDialog(data: any) {
    this.dialogref = this.dialogservice.open(SalesDetailsComponentComponent, {
      width: '1000px',
      maxHeight: '600px',
      data: data
    }).afterClosed().subscribe(x => {
      this.load();
      this.isLoading = false;
    });
  }

  openEditDialogNew() {
    this.dialogref = this.dialogservice.open(SalesDetailsComponentComponent, {
      width: '1000px',
      data: new SalesDto()
    }).afterClosed().subscribe(x => {
      this.load();
      this.isLoading = false;
    });
  }

  openSalesDetailsDialog() {
    this.dialogref = this.dialogservice.open(SalesDetailsComponentComponent, {
      width: '250px',
    })
  }
}
