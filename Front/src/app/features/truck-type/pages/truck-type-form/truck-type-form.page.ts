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
import { TruckTypeEndpoint } from '../../../../domain/truck-type/truck-type.endpoint';
import { TruckType } from '../../../../domain/truck-type/truck-type.models';
import { VolvoInputMessagesComponent } from '../../../../shared/components/volvo-input-messages/volvo-input-messages.component';

@Component({
  selector: 'app-truck-type-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatButtonModule, MatInputModule, MatSelectModule, MatFormFieldModule, ContentLoadingComponent, ContentAlertComponent, EnableLoadingDirective, VolvoInputMessagesComponent],
  templateUrl: './truck-type-form.page.html',
  styleUrl: './truck-type-form.page.scss'
})
export class TruckTypeFormPage implements OnInit, OnDestroy {
  errorMessage: string | undefined;
  private readonly sideNavigationService = inject(SideNavigationService);
  private readonly exampleEntityEndpoint = inject(TruckTypeEndpoint);

  protected entityId = this.sideNavigationService.State.Params.Id || "";
  protected getByIdCommand = this.exampleEntityEndpoint.getByIdCommand(() => this.entityId);
  protected submitCommand = this.exampleEntityEndpoint.saveCommand(() => this.bodyBuilder());

  protected formGroup: FormGroup = new FormGroup({});
  protected originalData: TruckType | null = null;

  /**
   *
   */
  constructor() {
  }

  formValidationMessages = {
    "Type": {
      "required": "Campo obrigatório",
      "maxlength": "Tamanho máximo de 50 caracteres"
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
      Type: new FormControl("", [Validators.required, Validators.maxLength(50)])
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

  private bodyBuilder(): TruckType {
    return {
      ...this.originalData,
      ...this.formGroup.value
    };
  }
}
