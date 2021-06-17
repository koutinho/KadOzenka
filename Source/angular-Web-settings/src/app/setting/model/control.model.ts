import {EnumControl} from "../enum/enum.control";

export class ControlModel<T>{
  value: T | undefined;
  key: string;
  label: string;
  controlType: EnumControl;

  constructor(options?: ControlModel<T>) {
    this.controlType = options?.controlType || EnumControl.simple;
    this.value = options?.value;
    this.key = options?.key || "";
    this.label = options?.label || "";
  }
}
