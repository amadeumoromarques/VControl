import { Directive, HostListener, inject } from '@angular/core';
import { NavigationService } from '../services/navigation.service';

@Directive({
  selector: '[navigationToggle]',
  standalone: true
})
export class NavigationToggleDirective {

  private readonly navigationService : NavigationService = inject(NavigationService);

  @HostListener('click', ['$event']) onClick($event : any){
    this.toggle();
}
  
	toggle() {
		this.navigationService.toggle();
	}

}
