import { Injectable, Component} from '@angular/core';
import { HttpModule, Http, Request, RequestMethod, Response, RequestOptions, Headers } from '@angular/http';
import { HttpClient, HttpResponse, HttpRequest, HttpHeaders, HttpClientModule } from '@angular/common/http';
import { Observable, Subject, ReplaySubject } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { DepartmentModel } from 'src/app/models/DepartmentModel';
import { Department } from 'src/app/models/Department';

@Component({
  providers: [Http]
})
  @Injectable({
    providedIn: 'root'
  })
export class DepartmentService {

  public _getUrl: string = ' /api/Department/GetDepartment ';
  public _getByIdUrl: string = '/api/Department/GetDepartment';
  public _deleteByIdUrl: string = '/api/Department/DeleteByID';
  public _saveUrl: string = '/api/Department/Save';
  public _updateurl: string = '/api/Department/Update';
  constructor(private _http: Http) {

  }
  getall(): Observable<DepartmentModel[]> {
    return this._http.get(this._getUrl)
      .pipe(map(this.extractData))
      .pipe(catchError(this.handleError));
  }
  getByID(id: string): Observable<DepartmentModel> {
    var getByIdUrl = this._getByIdUrl + '/' + id;
    return this._http.get(getByIdUrl)
      .pipe(map(this.extractData))
      .pipe(catchError(this.handleError));
  }
  save(department: DepartmentModel): Observable<string> {
    let body = JSON.stringify(department);
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let options = new RequestOptions({ headers: headers });
    return this._http.post(this._saveUrl, body, options)
      .pipe(map(this.extractData))
      .pipe(catchError(this.handleError));
  }
  edit(department: DepartmentModel): Observable<string> {

    return this._http.put(this._updateurl, department)
      .pipe(map(response => response.json().message))
      .pipe(catchError(this.handleError));

  }
  delete(id: string): Observable<string> {
    var deleteByIdUrl = this._deleteByIdUrl + '/' + id
    return this._http.delete(deleteByIdUrl)
      .pipe(map(response => response.json().message))
      .pipe(catchError(this.handleError));
  }
  private handleError(error: Response) {
    return Observable.throw(error.json().error || 'Opps!! Server error');
  }
  private extractData(res: Response) {
    let body = res.json();
    console.log(res.json())
    // return just the response, or an empty array if there's no data
    return body || [];
  }
}
 
//interface DepartmentModel {
//  Id: number;
//  DepartmentName: string;
//}
