import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';    
import { Employee }  from '../model/employee.model'

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private url = "https://localhost:5001";

  constructor(private http: HttpClient) { }

  getEmployee(){
    return this.http.get<Employee[]>(`${this.url}/api/EmployeeRecords`);
  }
}
