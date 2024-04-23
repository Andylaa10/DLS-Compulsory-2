import {Component, inject} from '@angular/core';
import {MatDialogRef} from "@angular/material/dialog";
import {MatButton, MatButtonModule} from "@angular/material/button";
import {MatFormField, MatFormFieldModule} from "@angular/material/form-field";
import {MatIcon, MatIconModule} from "@angular/material/icon";
import {MatInput, MatInputModule} from "@angular/material/input";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {CreatePatientDto} from "../../../core/dtos/createPatient.dto";

@Component({
  selector: 'app-create-patient-dialog',
  standalone: true,
  imports: [
    MatButton,
    MatButtonModule,
    MatFormField,
    MatIcon,
    MatInput,
    MatFormFieldModule, MatInputModule, MatIconModule, ReactiveFormsModule
  ],
  templateUrl: './create-patient-dialog.component.html',
  styleUrl: './create-patient-dialog.component.scss'
})
export class CreatePatientDialogComponent {
  private _dialogRef: MatDialogRef<CreatePatientDialogComponent> = inject(MatDialogRef<CreatePatientDialogComponent>);

  createPatientForm: FormGroup = new FormGroup({
    ssnControl: new FormControl(null, [Validators.max(10), Validators.min(10), Validators.required]),
    nameControl: new FormControl(null, [Validators.required]),
    emailControl: new FormControl(null, [Validators.email, Validators.required])
  });

  closeDialog(): void {
    this._dialogRef.close();
  }
}
