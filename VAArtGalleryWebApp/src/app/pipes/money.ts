
import {Pipe, PipeTransform, Inject, LOCALE_ID } from '@angular/core';
@Pipe({
  name: 'money',
})
export class MoneyPipe implements PipeTransform {

  constructor(@Inject(LOCALE_ID) private locale:string) {}

  transform(value: number): string {
    
    const formatter = new Intl.NumberFormat(this.locale, {
      style: 'currency',
      currency: 'EUR',
      currencyDisplay: 'code',
      useGrouping:true,
      minimumFractionDigits: 3,
      maximumFractionDigits: 3
    });

    let result = formatter.format(value);

    //return result;
    return result.replace(/\s/g, '.').replace('.EUR', ' EUR');
  }
}