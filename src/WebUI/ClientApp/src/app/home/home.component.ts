import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { ExportSalesReportQuery, Item, ItemCategory, ItemsListClient, SalesDto, SalesListClient } from '../cleanarchitecture-api';
import { SalesDetailsComponentComponent } from '../sales/_dialogs/sales-details-component/sales-details-component.component';
import { TdDialogService } from '@covalent/core/dialogs';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  displayedColumns: string[] = ['Sales_ID', 'Employee_ID', 'Remarks', 'Date', 'CreatedBy', 'LastModifiedBy', 'isDeleted'];
  dataSource: MatTableDataSource<SalesDto> = new MatTableDataSource<SalesDto>();
  dialogref: any;
  isLoading: boolean = false;
  filterCriteria: string;
  itemList: Item[] = [];
  itemCategoryLists = ItemCategory;
  date: Date = new Date();
  page: number;
  pageSize: number;
  totalRecord: number;
  productIdFilter: number;
  productCategoryFilter: string;
  predictedProductSales: number;
  predictedItemCategorySales: number;
  productNameFilter: string;

  highestSellingItem: string;
  highestSellingItemCategory: string;
  totalSalesCurrentMonth: number;

  @ViewChild(MatTable) table: MatTable<SalesDto>;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  constructor(private salesService: SalesListClient,
    private snackbar: MatSnackBar,
    private itemService: ItemsListClient,
    private tdDialogService: TdDialogService,
    private dialogservice: MatDialog) {
  }

  ngOnInit(): void {
    this.load();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator
  }

  load() {
    this.isLoading = true;
    this.getItems();
    this.getTotalSalesCurrentMonth();
    this.getHighestSellingItem();
    this.getHighestSellingItemCategory();
    this.salesService.getAllSalesRecordsQuery(undefined, undefined).subscribe(x => {
      this.dataSource = new MatTableDataSource(x);
      this.totalRecord = x.length;
      this.dataSource.paginator = this.paginator;
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
    this.itemService.getItemsBySearchCriteriaQuery(this.productIdFilter.toString()).subscribe(x => {
      this.productNameFilter = x[0].itemName;
    })
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
    })
  }
}
