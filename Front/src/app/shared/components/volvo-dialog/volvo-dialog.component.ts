import { ChangeDetectionStrategy, Component, Inject, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-volvo-dialog',
  templateUrl: './volvo-dialog.component.html',
  styleUrls: ['./volvo-dialog.component.css'],
  standalone: true,
  imports: [MatButtonModule, MatDialogModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class VolvoDialog {
  readonly dialogRef = inject(MatDialogRef<VolvoDialog>);

  constructor(@Inject(MAT_DIALOG_DATA) public data: { title: string; message: string }) { }
}
