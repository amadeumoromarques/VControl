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
import { TruckEndpoint } from '../../../../domain/truck/truck.endpoint';
import { Truck } from '../../../../domain/truck/truck.models';
import { MatSelectModule } from '@angular/material/select';
import { TruckTypeEndpoint } from '../../../../domain/truck-type/truck-type.endpoint';
import { ODataQueryCommand } from '@gmz/ngx-b-toolkit/odata';
import { TruckType } from '../../../../domain/truck-type/truck-type.models';
import { PlantOptions } from '../../../../domain/plant-options/plant-options.models';
import { PlantOptionsEndpoint } from '../../../../domain/plant-options/plant-options.endpoint';
import { ColorEndpoint } from '../../../../domain/color/color.endpoint';
import { Color } from '../../../../domain/color/color.models';
import { debounceTime, Subject, takeUntil } from 'rxjs';
import { VolvoInputMessagesComponent } from '../../../../shared/components/volvo-input-messages/volvo-input-messages.component';

@Component({
  selector: 'app-truck-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatButtonModule, MatInputModule, MatSelectModule, MatFormFieldModule, ContentLoadingComponent, ContentAlertComponent, EnableLoadingDirective, VolvoInputMessagesComponent],
  templateUrl: './truck-form.page.html',
  styleUrl: './truck-form.page.scss'
})
export class TruckFormPage implements OnInit, OnDestroy {
  errorMessage: string | undefined;
  truckTypeQueryCommand: ODataQueryCommand<TruckType>;
  plantOptionsQueryCommand: ODataQueryCommand<PlantOptions>;
  colorQueryCommand: ODataQueryCommand<Color>;

  private readonly sideNavigationService = inject(SideNavigationService);
  private readonly exampleEntityEndpoint = inject(TruckEndpoint);
  private onDestroy$ = new Subject<void>();

  protected entityId = this.sideNavigationService.State.Params.Id || "";
  protected getByIdCommand = this.exampleEntityEndpoint.getByIdCommand(() => this.entityId);
  protected submitCommand = this.exampleEntityEndpoint.saveCommand(() => this.bodyBuilder());
  protected formGroup: FormGroup = new FormGroup({});
  protected originalData: Truck | null = null;
  protected selectedColorId: string = "";

  /**
   *
   */
  constructor(private truckTypeEndpoint: TruckTypeEndpoint,
    private plantOptionsEndpoint: PlantOptionsEndpoint,
    private colorEndpoint: ColorEndpoint) {
    this.truckTypeQueryCommand = this.truckTypeEndpoint.getAllCommand();
    this.plantOptionsQueryCommand = this.plantOptionsEndpoint.getAllCommand();
    this.colorQueryCommand = this.colorEndpoint.getAllCommand();
  }

  formValidationMessages = {
    "ChassisCode": {
      "required": "Campo obrigatório",
      "maxlength": "Tamanho máximo de 80 caracteres"
    },
    "ManufacturerYear": {
      "required": "Campo obrigatório.",
      "max": "O Valor não pode exceder o ano atual.",
      "min": "Limite de cadastro mínimo de modelos do ano 2000 em diante."
    },
    "IdColor": {
      "required": "Campo obrigatório"
    },
    "IdTruckType": {
      "required": "Campo obrigatório"
    },
    "IdPlantOptions": {
      "required": "Campo obrigatório"
    }
  }

  ngOnInit(): void {
    this.truckTypeQueryCommand.execute();
    this.plantOptionsQueryCommand.execute();
    this.colorQueryCommand.execute();

    this.buildFormGroup();

    this.getByIdCommand.response$.subscribe(x => this.onGetByIdExecuted(x));
    if (this.sideNavigationService.State.Params.Id) {
      this.getByIdCommand.execute();
    }

    this.formGroup.get("IdColor")?.valueChanges
      .pipe(
        debounceTime(150),
        takeUntil(this.onDestroy$)
      )
      .subscribe((selectedColor: string) => {
        this.selectedColorId = selectedColor;
      });
  }

  onGetByIdExecuted(x: RestResponse<any, ErrorMessage>): void {
    if (x.isSuccess) {
      this.originalData = x.result;  // Armazena o objeto carregado
      this.formGroup.patchValue(x.result);
    }
  }

  buildFormGroup() {
    this.formGroup = new FormGroup({
      ChassisCode: new FormControl("", [Validators.required, Validators.maxLength(80)]),
      ManufacturerYear: new FormControl("", [Validators.required, Validators.min(2000), Validators.max(new Date().getFullYear())]),
      IdColor: new FormControl(null, [Validators.required]),
      IdTruckType: new FormControl(null, [Validators.required]),
      IdPlantOptions: new FormControl(null, [Validators.required])
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

  getColorSelected(options: Color[]) {
    if(options.length == 0){
      return null;
    }

    return options.find(opt => opt.Id == this.selectedColorId);
  }

  private bodyBuilder(): Truck {
    return {
      ...this.originalData,
      ...this.formGroup.value
    };
  }
}
