import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { NavigationService } from './services/navigation.service';
import {MatIconModule} from '@angular/material/icon';
import { NavigationToggleDirective } from './directives/navigation-toggle.directive';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navigation',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    NavigationToggleDirective,
    MatButtonModule
  ],
  templateUrl: './navigation.component.html',
  styleUrl: './navigation.component.scss'
})
export class NavigationComponent {
  protected readonly navigationBarService : NavigationService = inject(NavigationService);

  constructor(private router: Router) {}
  
  navigateToPage(page: string) {
    this.router.navigate([page]);
    
    this.navigationBarService.closeIfIsOnMobile();
  }
}
