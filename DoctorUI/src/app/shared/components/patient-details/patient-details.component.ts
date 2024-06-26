import {Component, inject, Input, OnChanges, signal, SimpleChanges} from '@angular/core';
import { Patient } from "../../../core/models/patient.model";
import { MatCard, MatCardContent, MatCardHeader, MatCardModule, MatCardTitleGroup } from "@angular/material/card";
import {
  MatExpansionModule,
  MatExpansionPanel,
  MatExpansionPanelDescription,
  MatExpansionPanelTitle
} from "@angular/material/expansion";
import {MatRipple} from "@angular/material/core";
import {MeasurementService} from "../../../core/services/measurement.service";
import {Measurement} from "../../../core/models/measurement.model";
import {MatProgressSpinner} from "@angular/material/progress-spinner";
import {DatePipe, JsonPipe} from "@angular/common";
import {MatCheckbox} from "@angular/material/checkbox";
import {UpdateMeasurementDto} from "../../../core/dtos/updateMeasurement.dto";
import {MatIcon} from "@angular/material/icon";
import {MomentDatePipe} from "../../../core/pipes/moment-date.pipe";

@Component({
  selector: 'app-patient-details',
  standalone: true,
  imports: [
    MatCard,
    MatCardHeader,
    MatCardTitleGroup,
    MatCardContent,
    MatCardModule,
    MatExpansionPanel,
    MatExpansionPanelTitle,
    MatExpansionPanelDescription,
    MatExpansionModule,
    MatRipple,
    MatProgressSpinner,
    DatePipe,
    MatCheckbox,
    MatIcon,
    MomentDatePipe,
    JsonPipe,
  ],
  templateUrl: './patient-details.component.html',
  styleUrl: './patient-details.component.scss'
})
export class PatientDetailsComponent implements OnChanges{
  @Input() patient!: Patient;

  loading = signal<boolean>(false);
  measurements = signal<Measurement[]>([]);

  private _measurementService: MeasurementService = inject(MeasurementService);

  ngOnChanges(changes: SimpleChanges): void {
    if (changes){
      if (this.patient){
        if(this.patient.ssn !== ''){
          this.getMeasurementsOnPatient(this.patient.ssn!);
        }
      }
    }
  }


  getMeasurementsOnPatient(ssn: string){
    this.measurements.set([]);
    this.loading.set(true);
    this._measurementService.getMeasurementOnPatient(ssn).subscribe((measurements: Measurement[]) => {
      this.measurements.set(measurements);
      this.loading.set(false);
    });
  }

  viewedByDoctor(measurement: Measurement) {
    const dto: UpdateMeasurementDto = {
      id: measurement.id,
      viewedByDoctor: true,
    };

    this._measurementService.updateMeasurement(dto.id, dto).subscribe(updatedMeasurement=>{
      const index = this.measurements().findIndex(m => m.id == dto.id);
      this.measurements()[index] = updatedMeasurement;
    });
  }
}
