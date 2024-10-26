import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, Input, OnDestroy, OnInit } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { ODataListResult, ODataQueryCommand } from '@gmz/ngx-b-toolkit/odata';
import { Subject, takeUntil } from 'rxjs';
import { EnableLoadingDirective } from '../../directives/enable-loading.directive';

@Component({
  selector: 'pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss'],
  standalone: true,
  imports: [CommonModule, MatIconModule, EnableLoadingDirective],
})
export class PaginationComponent implements OnInit, OnDestroy {

  private destroy$ = new Subject<void>();

  state : {
    result : ODataListResult<any> | undefined,
    totalItems : number,
    totalPages : number,
    itemsPerPage : number,
    currentPage: number,
    skipItems: number,
    paginationItemsArray : number[]
  } = {
    result : undefined,
    totalItems : 0,
    totalPages : 0,
    itemsPerPage : 0,
    currentPage: 0,
    skipItems: 0,
    paginationItemsArray: [] as number[]
  };

  @Input() queryCommand! : ODataQueryCommand<any>;

  constructor(private changeDetector : ChangeDetectorRef){
 
  }

  ngOnInit(): void {
    this.queryCommand?.response$.pipe(takeUntil(this.destroy$)).subscribe(x => {
      if (!x.isSuccess) return;
      this.state.result = x!.result;
      this.calculateCurrentPage();

    });
    
    // this.calculateCurrentPage();
  }

  ngOnDestroy(): void {
      this.destroy$.next();
  }

  goToPage(pageNumber : number){
    
    if (pageNumber <= 0) pageNumber = 0;
    if (pageNumber >= this.state.totalPages) pageNumber = this.state.totalPages;

    this.state.currentPage = pageNumber;
    this.queryCommand.state.queryString.param("PageNumber", pageNumber);
    
    this.queryCommand.execute();
  }

  calculateCurrentPage(){
    this.state.totalItems = this.state.result!.TotalCount;
    this.state.totalPages = this.state.result!.TotalPages;
    this.state.currentPage = this.state.result!.PageNumber;
    this.state.itemsPerPage = this.state.result!.PageSize;

    this.state.paginationItemsArray = [...this.calculatePreviousPageNumber(), this.state.currentPage, ...this.calculateNextPageNumber() ]
    
    console.log(this.state);
    // this.queryCommand.state.queryString.param("$top", this.state.itemsPerPage);
    // this.queryCommand.state.queryString.param("$skip", this.state.skipItems);
  }

  calculatePreviousPageNumber(){
    var items = [this.state.currentPage -1];
    return items.filter(x => x > 0);
  }

  calculateNextPageNumber(){
    var items = [this.state.currentPage + 1];
    return items.filter(x => x <= this.state.totalPages);
  }

}
