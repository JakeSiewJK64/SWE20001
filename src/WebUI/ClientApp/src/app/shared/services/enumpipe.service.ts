import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'enumpipe'
})
export class EnumpipeService implements PipeTransform {
  transform(value, args: string[]): any {
    let keys = [];
    for (var enumMember in value) {
      if (!isNaN(parseInt(enumMember, 10))) {
        keys.push({ key: parseInt(enumMember), value: value[enumMember] });
      }
    }
    return keys.reverse();
  }
}
