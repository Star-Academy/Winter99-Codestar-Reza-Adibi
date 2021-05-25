import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AddDocumentComponent } from './components/add-document/add-document.component';
import { HomeComponent } from './components/home/home.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { SearchResultsComponent } from './components/search-results/search-results.component';

const routes: Routes = [];

@NgModule({
  imports: [
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'search', component: SearchResultsComponent },
      { path: 'add', component: AddDocumentComponent },
      { path: '**', component: NotFoundComponent },
    ]),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
