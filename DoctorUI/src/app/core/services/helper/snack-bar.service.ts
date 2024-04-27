import {inject, Injectable} from '@angular/core';
import {MatSnackBar, MatSnackBarConfig} from "@angular/material/snack-bar";

@Injectable({
  providedIn: 'root'
})
export class SnackBarService {
  private _snackBar: MatSnackBar = inject(MatSnackBar);

  private snackBarConfig: MatSnackBarConfig = {
    duration: 3000,
    horizontalPosition: 'center',
    verticalPosition: 'top',
  };

  openSnackBar(message: string) {
    this._snackBar.open(message, 'Close', {
      ...this.snackBarConfig
    });
  }
}
