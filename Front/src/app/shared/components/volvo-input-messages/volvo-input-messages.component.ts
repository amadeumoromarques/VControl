import { Component, CUSTOM_ELEMENTS_SCHEMA, Host, Input, OnInit, SkipSelf } from '@angular/core';
import { FormGroupDirective, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'volvo-input-messages',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './volvo-input-messages.component.html',
  styleUrls: ['./volvo-input-messages.component.css']
})
export class VolvoInputMessagesComponent implements OnInit {
  @Input() control!: string;
  @Input() messages: { [key: string]: { [errorType: string]: string } } = {};
  @Input() onlyFirst = true;

  formControls!: { [key: string]: any };

  constructor(@Host() @SkipSelf() private formGroup: FormGroupDirective) {}

  ngOnInit() {
    this.formControls = this.formGroup.form.controls;
    console.log("Estou aqui!");
  }

  get hasError(): boolean {
    return this.formControlKeys().some(controlKey => this.isControlInvalid(controlKey));
  }

  get errors(): string[] {
    const errorMessages: string[] = [];
    for (const controlKey of this.formControlKeys()) {
      const controlErrors = this.getErrors(controlKey);
      for (const errorKey of this.objectToKeys(controlErrors)) {
        const errorMessage = this.messages[controlKey]?.[errorKey];
        if (errorMessage) {
          errorMessages.push(errorMessage);
          if (this.onlyFirst) break;
        }
      }
      if (this.onlyFirst && errorMessages.length) break;
    }

    console.log(errorMessages);
    return errorMessages;
  }

  private getErrors(controlKey: string): { [key: string]: any } {
    return this.formControls?.[controlKey]?.errors || {};
  }

  private isControlInvalid(controlKey: string): boolean {
    const control = this.formControls?.[controlKey];
    if (!control || !control.errors) return false;

    const hasMessages = this.objectToKeys(control.errors).some(
      errorKey => !!this.messages[controlKey]?.[errorKey]
    );
    return control.invalid && hasMessages && (control.dirty || control.touched);
  }

  private formControlKeys(): string[] {
    return this.control === '*' ? Object.keys(this.formControls) : [this.control];
  }

  private objectToKeys(value: { [key: string]: any }): string[] {
    return value ? Object.keys(value) : [];
  }
}
