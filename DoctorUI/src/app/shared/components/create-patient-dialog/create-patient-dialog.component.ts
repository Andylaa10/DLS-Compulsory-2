import {AfterViewInit, Component, ElementRef, inject, OnDestroy, ViewChild} from '@angular/core';
import {MatDialogRef} from "@angular/material/dialog";
import {MatButton, MatButtonModule} from "@angular/material/button";
import {MatFormField, MatFormFieldModule} from "@angular/material/form-field";
import {MatIcon, MatIconModule} from "@angular/material/icon";
import {MatInput, MatInputModule} from "@angular/material/input";
import {
  AbstractControl,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  ValidationErrors,
  Validators
} from "@angular/forms";
import {CreatePatientDto} from "../../../core/dtos/createPatient.dto";
import {PatientService} from "../../../core/services/patient.service";
import {catchError, fromEvent, of, Subject, takeUntil} from "rxjs";
import {SnackBarService} from "../../../core/services/helper/snack-bar.service";
import moment from "moment";

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

  //Create Patient form group
  createPatientForm: FormGroup = new FormGroup({
    ssnControl: new FormControl(null, [Validators.required, Validators.maxLength(10), Validators.minLength(10), Validators.pattern("^[0-9]*$"), this.validateSSNValidator()]),
    nameControl: new FormControl(null, [Validators.required]),
    emailControl: new FormControl(null, [Validators.required, Validators.email])
  });

  private _destroyed$: Subject<void> = new Subject<void>();


  private _patientService: PatientService = inject(PatientService);
  private _snackbarService: SnackBarService = inject(SnackBarService);


  closeDialog(): void {
    this._dialogRef.close();
  }

  createPatient() {
    const dto: CreatePatientDto = {
      email: this.createPatientForm.controls['emailControl'].value,
      ssn: this.createPatientForm.controls['ssnControl'].value,
      name: this.createPatientForm.controls['nameControl'].value,
    }

    this._patientService.createPatient(dto).pipe(catchError((err) => {
      this._snackbarService.openSnackBar(err['error'])
      return of(err);
    })).subscribe(() => {
      this._dialogRef.close();
    });

  }

  ngOnDestroy(): void {
    this._destroyed$.next();
    this._destroyed$.complete();
  }

  //Validate if the first 6 numbers are a valid date
  //Just like in denmark the ssn is based upon birthdate + 4 extra numbers
  //ex. 2305001111 = 23. March 2000
  validateSSNValidator(){
    return (control:AbstractControl) : ValidationErrors | null => {
      const ssn = control.value;

      if (!ssn) {
        return null;
      }

      const datePart = ssn.substring(0, 6);

      const parsedDate = moment(datePart, 'DDMMYYYY');

      if (!parsedDate.isValid()) {
        return { invalidDate: true };
      }

      return null;
    }
  }

}
