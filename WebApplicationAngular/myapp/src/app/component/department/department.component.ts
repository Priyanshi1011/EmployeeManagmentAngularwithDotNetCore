import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms'
import { DepartmentService } from './department.service';
import { DepartmentModel } from 'src/app/models/DepartmentModel';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  providers: [DepartmentService]
})
export class DepartmentComponent implements OnInit {
  public department: DepartmentModel;
  public departments: DepartmentModel[];
  public resmessage: string;
  employeeForm: FormGroup;
  constructor(private formBuilder: FormBuilder, private departmentService: DepartmentService) { }

  ngOnInit() {
    this.employeeForm = this.formBuilder.group({
      Id: 0,
      DepartmentName: new FormControl('', Validators.required)
      
    });
    //this.employees = this.employeeService.getall().pipe(
    //  tap(employee => this.employeeForm.patchValue(employee)));
    this.getAll(); 
  }
  onSubmit() {
    if (this.employeeForm.invalid) {
      return;
    }
    //this.getAll(); 
  }
  getAll() {
    //debugger  
    this.departmentService.getall().subscribe(
      response => {
        //console.log(response)  
        this.departments = response;
        //console.log(this.departments); 
      }, error => {
        console.log(error);
      }
    );
  }
  edit(e, m) {
    debugger  
    e.preventDefault();
    this.departmentService.getByID(m.id)
      .subscribe(response => {
        //console.log(response);  
        this.department = response;
        
        this.getAll();
        this.employeeForm.setValue({
          Id: this.department.id,
          DepartmentName: this.department.departmentName
        });
  
      }, error => {
        console.log(error);
      });
  }
  save() {
    //debugger  
    this.departmentService.save(this.employeeForm.value)
      .subscribe(response => {
        //console.log(response)  
        this.resmessage = response;
        this.getAll();
        this.reset();
      }, error => {
        console.log(error);
      });
  }
  delete(e, m) {
    //debugger  
    e.preventDefault();
    var IsConf = confirm('You are about to delete ' + m.lastName + '. Are you sure?');
    if (IsConf) {
      this.departmentService.delete(m.id)
        .subscribe(response => {
          //console.log(response)  
          this.resmessage = response;
          this.getAll();
        }, error => {
          console.log(error);
        });
    }
  }
  reset() {
    this.employeeForm.setValue({
      Id: 0,
      DepartmentName: null
     
    });
  }  
}
//interface DepartmentModel {
//  Id: number;
//  DepartmentName: string;
//}
