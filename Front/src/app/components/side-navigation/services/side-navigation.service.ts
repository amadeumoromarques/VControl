import { ComponentType } from '@angular/cdk/portal';
import { ApplicationRef, ComponentRef, inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SideNavigationService {

  private _state : SideNavigationState = new SideNavigationState();
  private _onClosed$ : Subject<any> = new Subject();

  public onSidenavToggleChanged$ : BehaviorSubject<SideNavigationState> = new BehaviorSubject(new SideNavigationState());
  get State() : SideNavigationState {
    return this._state;
  }

  get onSideNavigationClosed$() : Observable<SideNavigationState> {
    return this._onClosed$.asObservable();
  }

  constructor() { }

  open<T>(component: ComponentType<T>, params? : any) : Observable<any> {
    this.close();
    this._state.Component = component;
    this._state.Opened = true;
    this._state.Params = params;

    this.onSidenavToggleChanged$.next(this._state);
    return this._onClosed$.asObservable();
  }

  openState<T>(component: ComponentType<T>, params? : any): ComponentRef<T> | null {
    this._state.Component = component;
    this._state.Opened = true;
    this._state.Params = params;

    // Atualiza o estado para abrir o side navigation
    this.onSidenavToggleChanged$.next(this._state);

    // Retorne o ComponentRef quando o componente for instanciado no SideNavigationComponent
    return this._state.ComponentRef ?? null;
  }

  close(data? : any) {
    this._state.Component = undefined;
    this._state.Opened = false;
    this._state.Params = undefined;
    this.onSidenavToggleChanged$.next(this._state);
    this._onClosed$.next(data);
  }
}

export class SideNavigationState {
  Opened: boolean = false;
  Component?: ComponentType<any>;
  Params?: any;
  ComponentRef?: ComponentRef<any>; // Nova propriedade para armazenar o ComponentRef
}
