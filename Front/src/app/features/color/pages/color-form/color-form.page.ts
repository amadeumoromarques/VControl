import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { SideNavigationService } from '../../../../components/side-navigation/services/side-navigation.service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ContentLoadingComponent } from '../../../../components/content-loading/content-loading.component';
import { ContentAlertComponent } from '../../../../components/content-alert/content-alert.component';
import { EnableLoadingDirective } from '../../../../directives/enable-loading.directive';
import { RestResponse, ErrorMessage } from '@gmz/ngx-b-toolkit/common';
import { MatSelectModule } from '@angular/material/select';
import { ColorEndpoint } from '../../../../domain/color/color.endpoint';
import { Color } from '../../../../domain/color/color.models';
import { VolvoInputMessagesComponent } from '../../../../shared/components/volvo-input-messages/volvo-input-messages.component';

@Component({
  selector: 'app-color-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatInputModule,
    MatSelectModule,
    MatFormFieldModule,
    ContentLoadingComponent,
    ContentAlertComponent,
    EnableLoadingDirective,
    VolvoInputMessagesComponent],
  templateUrl: './color-form.page.html',
  styleUrl: './color-form.page.scss'
})
export class ColorFormPage implements OnInit, OnDestroy {
  errorMessage: string | undefined;
  private readonly sideNavigationService = inject(SideNavigationService);
  private readonly exampleEntityEndpoint = inject(ColorEndpoint);

  protected entityId = this.sideNavigationService.State.Params.Id || "";
  protected getByIdCommand = this.exampleEntityEndpoint.getByIdCommand(() => this.entityId);
  protected submitCommand = this.exampleEntityEndpoint.saveCommand(() => this.bodyBuilder());

  protected formGroup: FormGroup = new FormGroup({});
  protected originalData: Color | null = null;

  /**
   *
   */
  constructor() {
  }

  formValidationMessages = {
    "Name": {
      "required": "Campo obrigatório",
      "maxlength": "Tamanho máximo de 50 caracteres"
    },
    "SapCode": {
      "required": "Campo obrigatório",
      "maxlength": "Tamanho máximo de 50 caracteres"
    },
    "HexaColor": {
      "required": "Campo obrigatório"
    }
  }

  ngOnInit(): void {
    this.buildFormGroup();

    this.getByIdCommand.response$.subscribe(x => this.onGetByIdExecuted(x));
    if (this.sideNavigationService.State.Params.Id) {
      this.getByIdCommand.execute();
    }
  }

  onGetByIdExecuted(x: RestResponse<any, ErrorMessage>): void {
    if (x.isSuccess) {
      this.originalData = x.result;  // Armazena o objeto carregado
      this.formGroup.patchValue(x.result);
    }
  }

  buildFormGroup() {
    this.formGroup = new FormGroup({
      Name: new FormControl("", [Validators.required, Validators.maxLength(50)]),
      SapCode: new FormControl("", [Validators.required, Validators.maxLength(50)]),
      HexaColor: new FormControl("", [Validators.required])      
    });
  }

  ngOnDestroy(): void {
  }

  save() {
    this.submitCommand.execute();
    this.submitCommand.response$.subscribe(x => {
      if (x.isSuccess) {
        this.sideNavigationService.close();
      }
      if (x.error?.body?.ErrorCode)
      {
        this.errorMessage = x.error?.body?.ErrorCode;
      }
    }, error => {
      console.log("Erro ao salvar: ", error);
    })
  }

  private bodyBuilder(): Color {
    return {
      ...this.originalData,
      ...this.formGroup.value
    };
  }
}
