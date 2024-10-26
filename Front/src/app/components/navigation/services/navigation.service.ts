import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NavigationService {

  _collapsed : boolean = true;
  get Opened() : boolean {
    return !this._collapsed;
  }

  constructor() { }

  toggle(){
    this._collapsed = !this._collapsed;
  }

  open(){
    this._collapsed = false;
  }

  close() {
    this._collapsed = true;
  }

  isMobileViewport(): boolean {
    const width = window.innerWidth;
    // Considera-se um dispositivo móvel se a largura for menor que 768 pixels
    return width < 1200;
  }

  closeIfIsOnMobile() {
    if (this.isMobileViewport())
      {
        this.toggle();
      }
  }
}
