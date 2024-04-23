import {Component} from '@angular/core';
import {PatientTableComponent} from "../../shared/components/patient-table/patient-table.component";
import {PatientDetailsComponent} from "../../shared/components/patient-details/patient-details.component";
import {Patient} from "../../core/models/patient.model";
import {CdkDragDrop, CdkDropList} from "@angular/cdk/drag-drop";
import {MatDialogActions, MatDialogClose, MatDialogTitle} from "@angular/material/dialog";
import {MatButton} from "@angular/material/button";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    PatientTableComponent,
    PatientDetailsComponent,
    CdkDropList,
    MatDialogActions,
    MatDialogTitle,
    MatButton,
    MatDialogClose,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  selectedPatient!: Patient;

  drop($event: CdkDragDrop<Patient, any>) {
    if ($event.previousContainer !== $event.container) {
      this.selectedPatient = $event.previousContainer.data;
    }
  }
}
