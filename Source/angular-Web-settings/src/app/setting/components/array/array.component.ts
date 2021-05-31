import {Component, Input} from "@angular/core";
import { ControlModel } from "../../model/control.model";
import {CdkTextareaAutosize} from '@angular/cdk/text-field';

@Component({
  selector: 'ko-array',
  templateUrl: './array.component.ts.html',
})
export class ArrayControlComponent {
@Input() control!: ControlModel<string[]>;

constructor() {
}

getArrayValue(): string{
  return this.control.value?.join("\n") || "";
}
}
