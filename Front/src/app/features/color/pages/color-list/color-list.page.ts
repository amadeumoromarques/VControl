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
import { MatTooltipModule } from '@angular/material/tooltip';
import { ColorFormPage } from '../color-form/color-form.page';
import { Color } from '../../../../domain/color/color.models';
import { ColorEndpoint } from '../../../../domain/color/color.endpoint';

@Component({
  selector: 'app-color-list',
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
  templateUrl: './color-list.page.html',
  styleUrl: './color-list.page.scss'
})
export class ColorListPage implements OnInit, OnDestroy {

  readonly dialog = inject(MatDialog);
  private onDestroy$ = new Subject<void>();

  private readonly entityEndpoint: ColorEndpoint = inject(ColorEndpoint);
  private readonly sideNavigationService = inject(SideNavigationService);

  protected formGroup = new FormGroup({});

  getAllQueryCommand: ODataQueryCommand<Color> = this.entityEndpoint.getAllCommand();

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
    this.sideNavigationService.open(ColorFormPage, {
      // Amadeu : "Amadeu"
    }).pipe(first()).subscribe(x => {
          this.getAllQueryCommand.execute(); });
  }

  onDelete(id: string) {
    this.entityEndpoint.delete(id).pipe(first()).subscribe(x => {
      this.getAllQueryCommand.execute();
    });
  }

  private buildFormGroup() {
    this.formGroup.addControl("Search", new FormControl());
  }

  private listenToSearchFormControl() {
    this.formGroup.get("Search")?.valueChanges.pipe(debounceTime(1000), takeUntil(this.onDestroy$)).subscribe((x: string) => {
      var filterArray = x.split(" ").map(x => x.trim()).filter(x => x != " ").map(x => `Contains(SapCode, '${x}') or Contains(Name, '${x}')`).join(" or ");
      this.getAllQueryCommand.state.queryString.param("$filter", `${filterArray}`, true);
    });
  }
}
