import { ChangeDetectorRef, Component, ComponentRef, HostBinding, OnDestroy, OnInit, ViewChild, ViewContainerRef, AfterViewInit } from '@angular/core';
import { SideNavigationService, SideNavigationState } from './services/side-navigation.service';
import { Subject, takeUntil } from 'rxjs';
import { CommonModule } from '@angular/common';
import { MatIcon } from '@angular/material/icon';
import { MatTooltip } from '@angular/material/tooltip';

@Component({
  selector: 'app-side-navigation',
  templateUrl: './side-navigation.component.html',
  styleUrls: ['./side-navigation.component.scss'],
  standalone: true,
  imports: [CommonModule, MatIcon, MatTooltip]
})
export class SideNavigationComponent implements OnInit, OnDestroy, AfterViewInit {

  @ViewChild('dynamicComponentContainer', { read: ViewContainerRef }) container!: ViewContainerRef;
  componentRef: ComponentRef<any> | null = null;

  private onDestroy$ = new Subject<void>();
  @HostBinding('style.display') public display: string = 'none';

  sideNavigationState?: SideNavigationState;

  constructor(
    private sideNavigationService: SideNavigationService,
    public changeDetectorRef: ChangeDetectorRef
  ) {
    this.sideNavigationService.onSidenavToggleChanged$.pipe(takeUntil(this.onDestroy$)).subscribe(this.onSidenavToggleChanged.bind(this));
  }

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    if (this.sideNavigationState?.Opened) {
      this.loadComponent();
    }
  }

  ngOnDestroy(): void {
    this.onDestroy$.next();
    this.onDestroy$.complete();
  }

  loadComponent() {
    if (this.container) {
      this.container.clear(); // Limpa o conteúdo anterior

      if (this.sideNavigationService.State?.Component) {
        this.componentRef = this.container.createComponent(this.sideNavigationService.State.Component);
        this.componentRef.changeDetectorRef.markForCheck();
        
        if (this.componentRef.instance) {
          
          // Salva a referência no estado
          if (this.sideNavigationState) {
            this.sideNavigationState.ComponentRef = this.componentRef;
          }
        }
      }
    }
  }

  onSidenavToggleChanged(state: SideNavigationState) {
    this.sideNavigationState = state;
    if (state.Opened === true) {
      this.display = 'block';
      this.loadComponent();
    } else {
      this.display = 'none';
      this.container?.clear(); // Limpa o componente ao fechar
      console.log('@@@ fechou');
      
    }
  }

  close() {
    this.sideNavigationService.close();
    console.log('@@@ fechou 2');
  }
}
