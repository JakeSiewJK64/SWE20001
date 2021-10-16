import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'splitString'
})
export class SplitStringPipe implements PipeTransform {
  transform(value: any): any {
    try {
      if (value) {
        var result = value.match(/[A-Z][a-z]+/g);
        return result ? result.join(" ") : value;
      }
      return value;
    } catch {
      return value;
    }
  }
}