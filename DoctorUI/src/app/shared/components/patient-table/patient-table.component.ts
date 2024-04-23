import {
  AfterViewInit,
  Component,
  ElementRef,
  inject,
  OnDestroy,
  signal,
  ViewChild
} from '@angular/core';
import {
  MatTableDataSource,
  MatTableModule
} from "@angular/material/table";
import {Patient} from "../../../core/models/patient.model";
import {MatPaginator, PageEvent, MatPaginatorModule} from "@angular/material/paginator";
import {MatProgressBar} from "@angular/material/progress-bar";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {catchError, debounceTime, fromEvent, of, Subject, takeUntil} from "rxjs";
import {
  CdkDrag,
  CdkDropList,
} from "@angular/cdk/drag-drop";
import {MatIcon, MatIconModule} from "@angular/material/icon";
import {PaginatedResult} from "../../../core/models/helper/paginatedResult.model";
import {PatientService} from "../../../core/services/patient.service";
import {MatButtonModule, MatMiniFabButton} from "@angular/material/button";
import {MatTooltip, MatTooltipModule} from "@angular/material/tooltip";
import {MatDialog} from "@angular/material/dialog";
import {CreatePatientDialogComponent} from "../create-patient-dialog/create-patient-dialog.component";

@Component({
  selector: 'app-patient-table',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatTableModule,
    MatPaginatorModule,
    MatProgressBar,
    CdkDropList,
    CdkDrag,
    MatIcon,
    MatMiniFabButton,
    MatTooltip,
    MatButtonModule, MatTooltipModule, MatIconModule
  ],
  templateUrl: './patient-table.component.html',
  styleUrl: './patient-table.component.scss'
})
export class PatientTableComponent implements AfterViewInit, OnDestroy {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild('input', {static: false}) searchInput!: ElementRef;

  private _destroyed$: Subject<void> = new Subject<void>();
  private _patientService: PatientService = inject(PatientService);
  private _dialog: MatDialog = inject(MatDialog);

  displayedColumns: string[] = ['id', 'ssn', 'email', 'name', 'action'];
  dataSource!: MatTableDataSource<Patient>;
  pageNumber: number = 0;
  totalCount: number = 0;
  pageSize: number = 5;

  selectedPatient = signal<Patient>({
    email: '',
    id: 0,
    name: '',
    ssn: ''
  });
  loading = signal(false);

  pageChangeEvent($event: PageEvent): void {
    this.loading.set(true);

    //Updates the page size and number used for pagination
    this.pageSize = $event.pageSize;
    this.pageNumber = $event.pageIndex;


    this._patientService.paginatedPatients(this.pageNumber, this.pageSize).subscribe(result => {
      this.updateMoreDataSource(result);
    })

  }

  search(): void {
    fromEvent(this.searchInput.nativeElement, 'keyup').pipe(debounceTime(500), takeUntil(this._destroyed$)).subscribe(() => {
      const searchTerm = (this.searchInput.nativeElement as HTMLInputElement).value;

    })
  }

  ngAfterViewInit(): void {
    //Listen for search input field to get text
    this.search();

    //get patients
    this.getPatients(this.pageNumber, this.pageSize);
  }

  ngOnDestroy(): void {
    this._destroyed$.next();
    this._destroyed$.complete();
  }

  // Sets the selected patient, and send it to the home component
  setSelectedPatient(row: Patient): void {
    this.selectedPatient.set(row);
  }


  getPatients(pageNumber: number, pageSize: number) {
    this.loading.set(true);
    this._patientService.paginatedPatients(pageNumber, pageSize).pipe(takeUntil(this._destroyed$), catchError(() => {
      this.loading.set(false);
      return of();
    })).subscribe((patients: PaginatedResult<Patient>) => {
      this.updateDataSource(patients);
    })
  }

  updateDataSource(patients: PaginatedResult<Patient>) {
    this.dataSource = new MatTableDataSource<Patient>(patients.items);
    this.dataSource.paginator = this.paginator;
    this.totalCount = patients.totalCount;
    this.loading.set(false);
  }

  updateMoreDataSource(patients: PaginatedResult<Patient>) {
    this.dataSource = new MatTableDataSource<Patient>(patients.items);
    this.loading.set(false);
  }

  openCreatePatientDialog() {
    const dialog = this._dialog.open(CreatePatientDialogComponent, {
      enterAnimationDuration: 500,
      exitAnimationDuration: 500,
      width: '400px',
      height: '80%',
    })


    dialog.afterClosed().subscribe(result => {
      this.getPatients(this.pageNumber, this.pageSize);
    });
  }

  deletePatient(ssn: number) {
    const confirmation = window.confirm("Are you sure you want to delete this patient?");
    if (!confirmation) {
      return; // Do nothing if user cancels the deletion
    }

    this._patientService.deletePatient(ssn)
      .pipe(
        takeUntil(this._destroyed$),
        catchError(() => {
          this.loading.set(false);
          return of();
        })
      )
      .subscribe(result => {
        this.getPatients(this.pageNumber, this.pageSize);
      });
  }

}
