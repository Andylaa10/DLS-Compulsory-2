import {Component, inject, OnDestroy} from '@angular/core';
import {MatDialogRef} from "@angular/material/dialog";
import {MatButton, MatButtonModule} from "@angular/material/button";
import {MatFormField, MatFormFieldModule} from "@angular/material/form-field";
import {MatIcon, MatIconModule} from "@angular/material/icon";
import {MatInput, MatInputModule} from "@angular/material/input";
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {CreatePatientDto} from "../../../core/dtos/createPatient.dto";
import {PatientService} from "../../../core/services/patient.service";
import {catchError, of, Subject, takeUntil} from "rxjs";

@Component({
  selector: 'app-create-patient-dialog',
  standalone: true,
  imports: [
    MatButton,
    MatButtonModule,
    MatFormField,
    MatIcon,
    MatInput,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    ReactiveFormsModule,
    FormsModule,
  ],
  templateUrl: './create-patient-dialog.component.html',
  styleUrl: './create-patient-dialog.component.scss'
})
export class CreatePatientDialogComponent implements OnDestroy {
  private _dialogRef: MatDialogRef<CreatePatientDialogComponent> = inject(MatDialogRef<CreatePatientDialogComponent>);

  //Create Patient form group;
  createPatientForm: FormGroup = new FormGroup({
    ssnControl: new FormControl(null, [Validators.required, Validators.maxLength(10), Validators.minLength(10)]),
    nameControl: new FormControl(null, [Validators.required]),
    emailControl: new FormControl(null, [Validators.required, Validators.email])
  });

  private _destroyed$: Subject<void> = new Subject<void>();


  private _patientService: PatientService = inject(PatientService);

  closeDialog(): void {
    this._dialogRef.close();
  }

  createPatient() {
    const dto: CreatePatientDto = {
      email: this.createPatientForm.controls['emailControl'].value,
      ssn: this.createPatientForm.controls['ssnControl'].value,
      name: this.createPatientForm.controls['nameControl'].value,
    }

    this._patientService.createPatient(dto).pipe(takeUntil(this._destroyed$), catchError((err) => {
      return of(err);
    })).subscribe(() => {
      this._dialogRef.close();
    });

  }

  ngOnDestroy(): void {
    this._destroyed$.next();
    this._destroyed$.complete();
  }
}
