import { CommonModule } from '@angular/common';
import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { PaginationComponent } from '../../../../components/pagination/pagination.component';
import { ContentAlertComponent } from '../../../../components/content-alert/content-alert.component';
import { ContentLoadingComponent } from '../../../../components/content-loading/content-loading.component';
import { debounceTime, first, Subject, takeUntil } from 'rxjs';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ODataQueryCommand } from '@gmz/ngx-b-toolkit/odata';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { SideNavigationService } from '../../../../components/side-navigation/services/side-navigation.service';
import { VolvoDialog } from '../../../../shared/components/volvo-dialog/volvo-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { PlantOptionsEndpoint } from '../../../../domain/plant-options/plant-options.endpoint';
import { PlantOptions } from '../../../../domain/plant-options/plant-options.models';
import { PlantFormPage } from '../plant-form/plant-form.page';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  selector: 'app-plant-list',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    PaginationComponent,
    ContentAlertComponent,
    ContentLoadingComponent,
    ReactiveFormsModule,
    MatButtonModule,
    VolvoDialog,
    MatTooltipModule
  ],
  templateUrl: './plant-list.page.html',
  styleUrl: './plant-list.page.scss'
})
export class PlantListPage implements OnInit, OnDestroy {

  readonly dialog = inject(MatDialog);
  private onDestroy$ = new Subject<void>();

  private readonly entityEndpoint: PlantOptionsEndpoint = inject(PlantOptionsEndpoint);
  private readonly sideNavigationService = inject(SideNavigationService);

  protected formGroup = new FormGroup({});

  getAllQueryCommand: ODataQueryCommand<PlantOptions> = this.entityEndpoint.getAllCommand();

  ngOnInit(): void {
    this.getAllQueryCommand.executeOnAnyODataChange();

    this.buildFormGroup();
    this.listenToSearchFormControl();

    this.getAllQueryCommand.execute();
  }

  ngOnDestroy(): void {
    this.getAllQueryCommand.destroy();
  }

  onAdd() {
    this.sideNavigationService.open(PlantFormPage, {
      // Amadeu : "Amadeu"
    }).pipe(first()).subscribe(x => {
      this.getAllQueryCommand.execute();
    });
  }

  onEdit(id: string) {
    this.sideNavigationService.open(PlantFormPage, {
      Id: id
    }).pipe(first()).subscribe(x => {
      this.getAllQueryCommand.execute();
    });
  }

  onDelete(id: string) {
    this.entityEndpoint.delete(id).pipe(first()).subscribe(x => {
      console.log("Deleted: ", x);
      this.getAllQueryCommand.execute();
    });
  }

  private buildFormGroup() {
    this.formGroup.addControl("Search", new FormControl());
  }

  private listenToSearchFormControl() {
    this.formGroup.get("Search")?.valueChanges.pipe(debounceTime(1000), takeUntil(this.onDestroy$)).subscribe((x: string) => {
      var filterArray = x.split(" ").map(x => x.trim()).filter(x => x != " ").map(x => `Contains(DisplayName, '${x}')`).join(" or ");
      this.getAllQueryCommand.state.queryString.param("$filter", `${filterArray}`, true);
    });
  }
}
