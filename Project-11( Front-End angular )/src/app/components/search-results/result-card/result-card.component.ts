import { Input } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { TextDocument } from '../../../models/Document';

@Component({
  selector: 'app-result-card',
  templateUrl: './result-card.component.html',
  styleUrls: ['./result-card.component.scss'],
})
export class ResultCardComponent {
  @Input()
  public document!: TextDocument;
}
