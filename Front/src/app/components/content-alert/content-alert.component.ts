import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'content-alert',
  templateUrl: './content-alert.component.html',
  styleUrl: './content-alert.component.scss',
  standalone: true,
  imports: [CommonModule, MatIconModule]
})
export class ContentAlertComponent {
  @Input() type : 'INFO' | 'ERROR' | 'TIPS' | 'NONE' = 'INFO';
}
