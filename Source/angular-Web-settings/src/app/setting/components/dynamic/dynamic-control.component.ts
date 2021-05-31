import {Component, Input} from "@angular/core";
import {ControlModel} from "../../model/control.model";
import {EnumControl} from "../../enum/enum.control";

@Component({
    selector: 'ko-dynamic-control',
    templateUrl: './dynamic-control.component.html'
})
export class DynamicControlComponent {
  @Input() control!: ControlModel<any>;
constructor() {
}

getControlType(): EnumControl {
  return this.control.controlType;
}
}
