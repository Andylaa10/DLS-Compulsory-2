import {Country} from "../models/helper/country.model";
import {Injectable, signal} from "@angular/core";

@Injectable({ providedIn: 'root' })
export class CountryStore {
  private readonly countryState = {
    $country: signal<Country>({name: ''})
  }

  public readonly $country = this.countryState.$country.asReadonly();

  setCountry(country: Country){
    this.countryState.$country.set(country);
  }
}
