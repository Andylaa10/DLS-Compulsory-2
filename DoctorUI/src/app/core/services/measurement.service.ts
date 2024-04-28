import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Measurement} from "../models/measurement.model";
import {UpdateMeasurementDto} from "../dtos/updateMeasurement.dto";
import {environment} from "../../../assets/enviorment";

@Injectable({
  providedIn: 'root'
})
export class MeasurementService {
  private _apiEndpoint: string = environment.API_PROD_URL_MEASUREMENT

  private _http: HttpClient = inject(HttpClient);

  getMeasurementOnPatient(ssn: string) : Observable<Measurement[]>{
    return this._http.get<Measurement[]>(`${this._apiEndpoint}/${ssn}`);
  }

  updateMeasurement(id: number, dto: UpdateMeasurementDto): Observable<Measurement>{
    return this._http.put<Measurement>(`${this._apiEndpoint}/UpdateMeasurement/${id}`, dto);
  }
}

