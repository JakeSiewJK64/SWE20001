import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ModalModule } from 'ngx-bootstrap/modal';
import { SharedModule } from './shared/shared.module';
import { InventoryComponent } from './inventory/inventory.component';
import { SalesComponent } from './sales/sales.component';
import { SalesDetailsComponentComponent } from './sales/_dialogs/sales-details-component/sales-details-component.component';
import { EditSalesDetailsComponentComponent } from './sales/_dialogs/edit-sales-details-component/edit-sales-details-component.component';
import { NoPageComponent } from './NoPage/no-page/no-page.component';
import { ItemDetailsComponentComponent } from './inventory-dialogs/item-details-component/item-details-component.component';
import { CurrentMonthSalesReportViewDialogComponent } from './sales/_dialogs/current-month-sales-report-view-dialog/current-month-sales-report-view-dialog.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    NoPageComponent,
    InventoryComponent,
    SalesComponent,
    SalesDetailsComponentComponent,
    EditSalesDetailsComponentComponent,
    ItemDetailsComponentComponent,
    CurrentMonthSalesReportViewDialogComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FontAwesomeModule,
    HttpClientModule,
    SharedModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', pathMatch: 'full', redirectTo: 'home', },
      { path: 'home', component: HomeComponent, canActivate: [AuthorizeGuard] },
      { path: 'home/inventory', component: InventoryComponent, canActivate: [AuthorizeGuard] },
      { path: 'home/sales', component: SalesComponent, canActivate: [AuthorizeGuard] },
      { path: '**', pathMatch: 'full', 
        component: NoPageComponent },
    ], { relativeLinkResolution: 'legacy' }),
    BrowserAnimationsModule,
    ModalModule.forRoot()
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
