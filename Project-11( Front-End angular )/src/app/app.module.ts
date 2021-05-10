import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatButtonModule } from '@angular/material/button';
import { HttpClientModule, HttpParams } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { LogoComponent } from './components/logo/logo.component';
import { SearchResultsComponent } from './components/search-results/search-results.component';
import { SearchBarComponent } from './components/search-bar/search-bar.component';
import { ResultCardComponent } from './components/search-results/result-card/result-card.component';
import { ResultPanelComponent } from './components/search-results/result-panel/result-panel.component';
import { AddDocumentComponent } from './components/add-document/add-document.component';
import { NotFoundComponent } from './components/not-found/not-found.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LogoComponent,
    SearchResultsComponent,
    SearchBarComponent,
    ResultCardComponent,
    ResultPanelComponent,
    AddDocumentComponent,
    NotFoundComponent,
  ],
  imports: [BrowserModule, AppRoutingModule, MatButtonModule, HttpClientModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
