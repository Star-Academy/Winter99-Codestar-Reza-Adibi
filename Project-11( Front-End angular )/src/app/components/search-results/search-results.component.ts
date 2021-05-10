import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TextDocument } from 'src/app/models/Document';
import { SearchService } from 'src/app/services/search/search.service';

@Component({
  selector: 'app-search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.scss'],
})
export class SearchResultsComponent implements OnInit {
  public documents: TextDocument[] | null = [];
  public seletedDocument: TextDocument | undefined;
  public searchQuery: string;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private searchService: SearchService
  ) {
    this.searchQuery = this.getQueryFromUri();
  }

  ngOnInit(): void {
    this.checkQuery();
    this.getSrarchResults();
  }

  checkQuery(): void {
    if (this.searchQuery === null || this.searchQuery === '') {
      this.router.navigate(['/']);
    }
  }

  private getQueryFromUri(): string {
    let searchQuery;
    this.route.queryParamMap.subscribe(
      (params) => (searchQuery = params.get('query'))
    );
    return searchQuery ? searchQuery : '';
  }

  selectCard(document: TextDocument): void {
    this.unselectCard();
    this.seletedDocument = document;
  }

  unselectCard(): void {
    this.seletedDocument = undefined;
  }

  getSrarchResults(): void {
    const results = this.searchService.search(this.searchQuery);
    results.subscribe(
      (documents) =>
        (this.documents = documents.length === 0 ? null : documents)
    );
  }
}
