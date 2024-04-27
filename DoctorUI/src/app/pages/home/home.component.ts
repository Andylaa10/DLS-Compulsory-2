import {Component, inject, OnInit} from '@angular/core';
import {PatientTableComponent} from "../../shared/components/patient-table/patient-table.component";
import {PatientDetailsComponent} from "../../shared/components/patient-details/patient-details.component";
import {Patient} from "../../core/models/patient.model";
import {CdkDragDrop, CdkDropList} from "@angular/cdk/drag-drop";
import {MatDialog, MatDialogActions, MatDialogClose, MatDialogTitle} from "@angular/material/dialog";
import {MatButton} from "@angular/material/button";
import {
  CreatePatientDialogComponent
} from "../../shared/components/create-patient-dialog/create-patient-dialog.component";
import {CountryDialogComponent} from "../../shared/components/country-dialog/country-dialog.component";

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
export class HomeComponent implements OnInit{
  selectedPatient!: Patient;

  private _dialog: MatDialog = inject(MatDialog);

  drop($event: CdkDragDrop<Patient, any>) {
    if ($event.previousContainer !== $event.container) {
      this.selectedPatient = $event.previousContainer.data;
    }
  }

  ngOnInit(): void {

    // Added timeout to appear as a popup, this way you have to pick a country before using the features
    setTimeout(()=>{
      const dialog = this._dialog.open(CountryDialogComponent, {
        disableClose: true,
        enterAnimationDuration: 500,
        exitAnimationDuration: 500,
        width: '400px',
        height: '300px',
      });
    },2000);

  }
}
