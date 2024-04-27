import { HttpInterceptorFn } from '@angular/common/http';
import {inject} from "@angular/core";
import {CountryStore} from "../state/country.state";

export const headerInterceptor: HttpInterceptorFn = (req, next) => {
  //Get selected country
  const _countryStore: CountryStore = inject(CountryStore);


  if (_countryStore.$country().name !== ''){
    const countryReq = req.clone({
      setHeaders: {
        Country: _countryStore.$country().name
      }
    });

    return next(countryReq);
  }

  return next(req);
};
