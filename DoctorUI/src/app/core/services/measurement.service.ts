import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Measurement} from "../models/measurement.model";
import {UpdateMeasurementDto} from "../dtos/updateMeasurement.dto";

@Injectable({
  providedIn: 'root'
})
export class MeasurementService {
  private _apiEndpoint: string = "http://localhost:9090/api/Measurement"

  private _http: HttpClient = inject(HttpClient);

  getMeasurementOnPatient(ssn: string) : Observable<Measurement[]>{
    return this._http.get<Measurement[]>(`${this._apiEndpoint}/${ssn}`);
  }

  updateMeasurement(id: number, dto: UpdateMeasurementDto): Observable<Measurement>{
    return this._http.put<Measurement>(`${this._apiEndpoint}/UpdateMeasurement/${id}`, dto);
  }
}

