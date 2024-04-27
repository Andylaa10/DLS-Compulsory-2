import {Component, inject} from '@angular/core';
import {CountryStore} from "../../../core/state/country.state";
import {MatButton} from "@angular/material/button";
import {MatDialog, MatDialogRef} from "@angular/material/dialog";
import {CountryDialogComponent} from "../../components/country-dialog/country-dialog.component";

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    MatButton
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  countryStore: CountryStore = inject(CountryStore);

  private _dialog: MatDialog = inject(MatDialog);

  changeCountry() {
    const dialog = this._dialog.open(CountryDialogComponent, {
      disableClose: true,
      enterAnimationDuration: 500,
      exitAnimationDuration: 500,
      width: '400px',
      height: '300px',
    });
  }
}
