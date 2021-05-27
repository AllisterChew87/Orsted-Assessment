import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../services/employee.service'
import { Employee }  from '../model/employee.model'

@Component({
  selector: 'app-employee-records',
  templateUrl: './employee-records.component.html',
  styleUrls: ['./employee-records.component.scss'],
  providers: [EmployeeService]
})
export class EmployeeRecordsComponent implements OnInit {
  employees : Employee[];

  constructor(private employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.getEmployee();
  }

  getEmployee(){
    this.employeeService.getEmployee().subscribe( response => { this.employees = response; });
  }
}
