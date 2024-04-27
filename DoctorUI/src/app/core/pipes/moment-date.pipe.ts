import { Pipe, PipeTransform } from '@angular/core';
import moment from "moment";

@Pipe({
  name: 'momentDate',
  standalone: true
})
export class MomentDatePipe implements PipeTransform {

  transform(value: moment.Moment): string {
    if (value.toString().length > 16){
      return value.toString().slice(0,16);
    }
    return value.toString();
  }

}
