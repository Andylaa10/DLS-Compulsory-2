import {AfterViewInit, Component, inject, Input, OnDestroy, ViewChild} from '@angular/core';
import {Country} from "../../../core/models/helper/country.model";
import {FormControl, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatInputModule} from "@angular/material/input";
import {MatSelectModule} from "@angular/material/select";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatAutocompleteModule} from "@angular/material/autocomplete";
import {MatIcon} from "@angular/material/icon";
import {MatButton} from "@angular/material/button";
import {debounceTime, Subject, takeUntil} from "rxjs";
import {MatDialogRef} from "@angular/material/dialog";
import {CountryStore} from "../../../core/state/country.state";
import {SnackBarService} from "../../../core/services/helper/snack-bar.service";

@Component({
  selector: 'app-country-dialog',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    FormsModule,
    MatAutocompleteModule,
    ReactiveFormsModule,
    MatIcon,
    MatButton
  ],
  templateUrl: './country-dialog.component.html',
  styleUrl: './country-dialog.component.scss'
})
export class CountryDialogComponent implements AfterViewInit, OnDestroy {
  private _initialCountries = [
    {"name": "Albania"},
    {"name": "Andorra"},
    {"name": "Armenia"},
    {"name": "Austria"},
    {"name": "Azerbaijan"},
    {"name": "Belarus"},
    {"name": "Belgium"},
    {"name": "Bosnia and Herzegovina"},
    {"name": "Bulgaria"},
    {"name": "Croatia"},
    {"name": "Cyprus"},
    {"name": "Czech Republic"},
    {"name": "Denmark"},
    {"name": "Estonia"},
    {"name": "Finland"},
    {"name": "France"},
    {"name": "Georgia"},
    {"name": "Germany"},
    {"name": "Greece"},
    {"name": "Hungary"},
    {"name": "Iceland"},
    {"name": "Ireland"},
    {"name": "Italy"},
    {"name": "Kazakhstan"},
    {"name": "Kosovo"},
    {"name": "Latvia"},
    {"name": "Liechtenstein"},
    {"name": "Lithuania"},
    {"name": "Luxembourg"},
    {"name": "Malta"},
    {"name": "Moldova"},
    {"name": "Monaco"},
    {"name": "Montenegro"},
    {"name": "Netherlands"},
    {"name": "North Macedonia"},
    {"name": "Norway"},
    {"name": "Poland"},
    {"name": "Portugal"},
    {"name": "Romania"},
    {"name": "Russia"},
    {"name": "San Marino"},
    {"name": "Serbia"},
    {"name": "Slovakia"},
    {"name": "Slovenia"},
    {"name": "Spain"},
    {"name": "Sweden"},
    {"name": "Switzerland"},
    {"name": "Turkey"},
    {"name": "Ukraine"},
    {"name": "United Kingdom"},
    {"name": "Vatican City"}
  ];

  @Input() countries: Country[] = this._initialCountries;

  private _destroyed$: Subject<void> = new Subject();
  private _countryStore: CountryStore = inject(CountryStore);
  private _snackbarService: SnackBarService = inject(SnackBarService);

  countryControl: FormControl = new FormControl<Country | null>(null, [Validators.required]);

  private _dialogRef: MatDialogRef<CountryDialogComponent> = inject(MatDialogRef<CountryDialogComponent>);


  clearCountry() {
    this.countryControl.setValue(null);
  }

  saveCountry(): void {
    const country = this.countries.find(c => c.name === this.countryControl.value)
    if (country){
      this._countryStore.setCountry(country);
      this._dialogRef.close();
      this._snackbarService.openSnackBar(`Chosen ${country.name}`)
    }
  }

  ngAfterViewInit(): void {
    this.search();
  }

  search(): void {
    this.countryControl.valueChanges.pipe(takeUntil(this._destroyed$), debounceTime(500)).subscribe(search => {
      if (search) {
        this.countries = this._initialCountries;
        this.countries = this.countries.filter(c => c.name.toLowerCase().includes(search.toLowerCase()));
      } else {
        this.countries = this._initialCountries;
      }
    });
  }

  ngOnDestroy(): void {
    this._destroyed$.next();
    this._destroyed$.complete();
  }
}
