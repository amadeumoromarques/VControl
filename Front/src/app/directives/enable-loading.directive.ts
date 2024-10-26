import { Directive, Input, Renderer2, ElementRef, OnChanges } from '@angular/core';

@Directive({
  selector: '[enableLoading]',
  standalone: true
})
export class EnableLoadingDirective implements OnChanges {

  @Input() enableLoading?: boolean = false;

  constructor(private el: ElementRef, private renderer: Renderer2) {}

  ngOnChanges() {
    if (this.enableLoading) {
      this.renderer.addClass(this.el.nativeElement, 'loading-button');
    } else {
      this.renderer.removeClass(this.el.nativeElement, 'loading-button');
    }
  }
}