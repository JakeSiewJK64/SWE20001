import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { SalesDto, SalesListClient } from '../cleanarchitecture-api';
import { SalesDetailsComponentComponent } from '../sales/_dialogs/sales-details-component/sales-details-component.component';
import { TdDialogService } from '@covalent/core/dialogs';
import { MatPaginator } from '@angular/material/paginator';
import { Subject } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  displayedColumns: string[] = ['Sales_ID', 'Date', 'Employee_ID', 'Remarks', 'isDeleted', 'CreatedBy', 'LastModifiedBy'];
  dataSource: MatTableDataSource<SalesDto> = new MatTableDataSource<SalesDto>();
  dialogref: any;
  isLoading: boolean = false;
  filterCriteria: string;
  date: Date = new Date();
  page: number;
  pageSize: number;
  totalRecord: number;

  @ViewChild(MatTable) table: MatTable<SalesDto>;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  constructor(private salesService: SalesListClient,
    private snackbar: MatSnackBar,
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
    this.salesService.getAllSalesRecordsQuery().subscribe(x => {
      this.dataSource = new MatTableDataSource(x);
      this.totalRecord = x.length;
      this.dataSource.paginator = this.paginator;
      this.isLoading = false;
    });
  }

  generateCSV() {
    this.salesService.generateMonthlySalesReportCommand(new Date()).subscribe(x => {
      var url = window.URL.createObjectURL(x.data);
      var a = document.createElement('a');
      document.body.appendChild(a);
      a.setAttribute('style', 'display: none');
      a.href = url;
      a.download = x.fileName;
      a.click();
      window.URL.revokeObjectURL(url);
      a.remove();
      this.snackbar.open("Generating CSV Report", "OK", { duration: 5000 });
    }, err => {
      this.tdDialogService.openAlert({
        title: "Oops!",
        message: err
      })
    });
  }

  filterSales() {
    
    if(this.filterCriteria.length < 1) {
      this.load();
      return;
    }

    this.salesService.getSalesByIdQuery(this.filterCriteria).subscribe(x => {
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
