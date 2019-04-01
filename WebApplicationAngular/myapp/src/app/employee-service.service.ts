import { Injectable, Component } from '@angular/core';
import { HttpModule, Http, Request, RequestMethod, Response, RequestOptions, Headers } from '@angular/http';  
import { HttpClient,HttpResponse,HttpRequest,HttpHeaders, HttpClientModule} from '@angular/common/http';
import { Observable, Subject, ReplaySubject } from 'rxjs';
import { map, catchError } from 'rxjs/operators';  
import { Employee } from './models/Employee'
import { EmployeeModel } from 'src/app/models/EmployeeModel';

@Component({
  providers: [Http]
})  
@Injectable({
  providedIn: 'root'
})


export class EmployeeServiceService {

  public headers: Headers;
  public _getUrl: string = ' /api/Employee/GetEmployee ';
  public _getByIdUrl: string = '/api/Employee/GetEmployee';
  public _deleteByIdUrl: string = '/api/Employee/DeleteByID';
  public _saveUrl: string = '/api/Employee/Save';
  public _updateurl: string = '/api/Employee/Update';

  constructor(private _http: Http) { /*this.headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8' }); */}
  //Get

  getall(): Observable<EmployeeModel[]> {
    return this._http.get(this._getUrl)
      .pipe(map(this.extractData))
      .pipe(catchError(this.handleError));
  }


  //getall() {
  //  return this._http.get(this._getUrl, { headers: this.headers });
     
  //}



  //getall(): Observable<EmployeeModel[]> {
  //  return this._http.get(this._getUrl)
  //    .pipe(map(res => <EmployeeModel[]>res.json()))
  //    .pipe(catchError(this.handleError));
  //}



  //GetByID
  getByID(id: string): Observable<EmployeeModel> {
    var getByIdUrl = this._getByIdUrl + '/' + id;
    return this._http.get(getByIdUrl)
      .pipe(map(this.extractData))
      .pipe(catchError(this.handleError));
  }

  //getByID(id: string): Observable<EmployeeModel> {
  //  var getByIdUrl = this._getByIdUrl + '/' + id;
  //  return this._http.get(getByIdUrl)
  //    .pipe(map(res => <EmployeeModel>res.json()))
  //    .pipe(catchError(this.handleError));
  //}


  //Post
  //save(employee: EmployeeModel): Observable<string> {

  //  let body = JSON.stringify(employee);
  //  let headers = new Headers({ 'Content-Type': 'application/json' });
  //  let options = new RequestOptions({ headers: headers });
  //  return this._http.post(this._saveUrl, body, options)
  //    .pipe(map(this.extractData))
  //    .pipe(catchError(this.handleError));
  //}

    save(employee: EmployeeModel) {
    return this._http.post(this._saveUrl, employee)
      .pipe(map(this.extractData))
      .pipe(catchError(this.handleError));
  }
  //save(employee: EmployeeModel): Observable<string> {
  //  let body = JSON.stringify(employee);
  //  let headers = new Headers({ 'Content-Type': 'application/json' });
  //  let options = new RequestOptions({ headers: headers });
  //  return this._http.post(this._saveUrl, body, options)
  //    .pipe(map(res => res.json().message))
  //    .pipe(catchError(this.handleError));
  //}

  edit(employee: EmployeeModel): Observable<string> {
    
    return this._http.put(this._updateurl,employee)
      .pipe(map(response => response.json().message))
      .pipe(catchError(this.handleError));

  }
  //Delete
   delete(id: string): Observable<string> {
    var deleteByIdUrl = this._deleteByIdUrl + '/' + id
    return this._http.delete(deleteByIdUrl)
      .pipe(map(response => response.json().message))
      .pipe(catchError(this.handleError));
  }
  //delete(id: string): Observable<string> {
  //  var deleteByIdUrl = this._deleteByIdUrl + '/' + id
  //  return this._http.delete(deleteByIdUrl)
  //    .pipe(map(response => response.json().message))
  //    .pipe(catchError(this.handleError));
  //}

 

  private handleError(error: Response) {
    return Observable.throw(error.json().error || 'Opps!! Server error');
  }
  private extractData(res: Response) {
    let body = res.json();
    // return just the response, or an empty array if there's no data
    return body || [];
  }


 











  
}
