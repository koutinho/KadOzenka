import { Injectable } from "@angular/core";
import { ControlModel } from "../model/control.model";
import { FormControl, FormGroup } from "@angular/forms";

@Injectable()
export class ControlService{
 constructor() {
 }
 toFormGroup(controls: ControlModel<string>[]){
   const group: any = {};
   controls.forEach(control => group[control.key] = new FormControl(control.value || ""))
   return new FormGroup(group);
 }
}
