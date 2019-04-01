import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EmployeeComponent } from './component/employee/employee.component';
import { DepartmentComponent } from './component/department/department.component';
import {PageNotFoundComponent } from './page-not-found/page-not-found.component';

const routes: Routes = [/*{ path: '', pathMatch: 'full', redirectTo: 'Employee' },*/
  { path: 'Employee', component: EmployeeComponent },
  { path: 'Department', component: DepartmentComponent },
  //{path:'api/Department',component:DepartmentComponent},
  {
    path: "**",
    component: PageNotFoundComponent
  }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
