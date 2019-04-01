import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import {EmployeeModel } from 'src/app/models/EmployeeModel';
import { EmployeeServiceService } from 'src/app/employee-service.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';  
import { ReactiveFormsModule } from '@angular/forms'
import { tap } from 'rxjs/operators';
@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  providers: [EmployeeServiceService]
})
export class EmployeeComponent implements OnInit {

  public employee: EmployeeModel;
  public employees: EmployeeModel[];
  public resmessage: string;
  employeeForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private employeeService: EmployeeServiceService) { }  


  ngOnInit() {
    this.employeeForm = this.formBuilder.group({
      Id: 0,
      LastName: new FormControl('', Validators.required),
      FirstName: new FormControl('', Validators.required),
      Address: new FormControl('', Validators.required),
      Qualification: new FormControl('', Validators.required),
      ContectNumber: new FormControl('', Validators.required)
    });
    //this.employees = this.employeeService.getall().pipe(
    //  tap(employee => this.employeeForm.patchValue(employee)));
    this.getAll();  
  }
  onSubmit() {
    if (this.employeeForm.invalid) {
      return;
    }
  }  
  //Get All Employee  
  getAll() {
    //debugger  
    this.employeeService.getall().subscribe(
      response => {
        console.log(response)  
        this.employees= response;
      }, error => {
        console.log(error);
      }
    );
  }
  //Get by ID  
  edit(e, m) {
    //debugger  
    e.preventDefault();
    this.employeeService.getByID(m.id)
      .subscribe(response => {
        //console.log(response);  
        this.employee = response;
        this.employeeForm.setValue({
          Id: this.employee.id,
          LastName: this.employee.lastName,
          FirstName: this.employee.firstName,
          Address: this.employee.address,
          Qualification: this.employee.qualification,
          ContectNumber: this.employee.contectNumber
        });
      }, error => {
        console.log(error);
      });
  }

  //Save Form  
  save() {
    //debugger  
    this.employeeService.save(this.employeeForm.value)
      .subscribe(response => {
        //console.log(response)  
        this.resmessage = response;
        this.getAll();
        this.reset();
      }, error => {
        console.log(error);
      });
  }

  //Delete  
  delete(e, m) {
    //debugger  
    e.preventDefault();
    var IsConf = confirm('You are about to delete ' + m.lastName + '. Are you sure?');
    if (IsConf) {
      this.employeeService.delete(m.id)
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
      LastName: null,
      FirstName: null,
      Address: null,
      Qualification: null,
      ContectNumber: null
    });
  }  
  
}
