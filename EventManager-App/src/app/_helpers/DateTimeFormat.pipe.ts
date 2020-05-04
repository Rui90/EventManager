import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';
import { AppConstants } from '../util/AppConstants';

@Pipe({
  name: 'DateTimeFormat'
})
export class DateTimeFormatPipe extends DatePipe implements PipeTransform {

  transform(value: any, args?: any): any {
    return super.transform(value, AppConstants.DATE_TIME_FMT);
  }

}
