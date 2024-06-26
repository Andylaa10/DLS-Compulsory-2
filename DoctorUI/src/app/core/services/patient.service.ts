import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {Patient} from "../models/patient.model";
import {PaginatedResult} from "../models/helper/paginatedResult.model";
import {CreatePatientDto} from "../dtos/createPatient.dto";
import {environment} from "../../../assets/enviorment";

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private _apiEndpoint: string = environment.API_PROD_URL_PATIENT;

  private _http: HttpClient = inject(HttpClient);

  getPatients(): Observable<Patient[]> {
    return this._http.get<Patient[]>(`${this._apiEndpoint}`);
  }

  getPatientBySSN(ssn: string): Observable<Patient> {
    return this._http.get<Patient>(`${this._apiEndpoint}/${ssn}`);
  }

  searchPatients(searchTerm: string, pageNumber: number, pageSize: number): Observable<PaginatedResult<Patient>> {
    return this._http.get<PaginatedResult<Patient>>(`${this._apiEndpoint}/SearchPatients?searchTerm=${searchTerm}&pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  paginatedPatients(pageNumber: number, pageSize: number): Observable<PaginatedResult<Patient>> {
    return this._http.get<PaginatedResult<Patient>>(`${this._apiEndpoint}/GetPatientPage?pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  createPatient(dto: CreatePatientDto): Observable<Patient> {
    return this._http.post<Patient>(`${this._apiEndpoint}/CreatePatient`, dto);
  }

  deletePatient(ssn: number): Observable<Patient> {
    return this._http.delete<Patient>(`${this._apiEndpoint}/DeletePatient/` + ssn);
  }

}
