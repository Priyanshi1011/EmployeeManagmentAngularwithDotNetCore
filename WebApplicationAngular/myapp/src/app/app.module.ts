import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule, Http, Request, RequestMethod, Response, RequestOptions, Headers } from '@angular/http';
import { ReactiveFormsModule } from '@angular/forms';
//import {HashLocationStrategy,LocationStrategy } from '@angular/common';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EmployeeComponent } from './component/employee/employee.component';
import { EmployeeServiceService } from './employee-service.service';
import { Routes } from '@angular/router';
import { DepartmentComponent } from './component/department/department.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';


const routes: Routes = [
  { path: 'api/Employee', component: EmployeeComponent }
];  
@NgModule({
  declarations: [
    AppComponent,
    EmployeeComponent,
    DepartmentComponent,
    PageNotFoundComponent
  
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [EmployeeServiceService,],
  bootstrap: [AppComponent]
})
export class AppModule { }
