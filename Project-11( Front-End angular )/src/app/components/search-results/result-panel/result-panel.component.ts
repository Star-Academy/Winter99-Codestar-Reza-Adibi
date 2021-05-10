import { Component, Input, OnInit } from '@angular/core';
import { TextDocument } from 'src/app/models/Document';

@Component({
  selector: 'app-result-panel',
  templateUrl: './result-panel.component.html',
  styleUrls: ['./result-panel.component.scss'],
})
export class ResultPanelComponent implements OnInit {
  public opacity: number = 0;
  public display: string = 'none';
  private _document?: TextDocument;
  private faidingDuration: number = 300;

  @Input()
  public set document(newDocument: TextDocument | undefined) {
    if (newDocument !== undefined) {
      this._document = newDocument;
      console.log(newDocument);
      this.showDocument();
    }
  }

  public get document(): TextDocument | undefined {
    return this._document === undefined
      ? new TextDocument('', '')
      : this._document;
  }

  constructor() {}

  ngOnInit(): void {}

  showDocument() {
    this.display = 'flex';
    setTimeout(() => {
      this.opacity = 100;
    }, this.faidingDuration);
  }

  hidePanel() {
    this.opacity = 0;
    setTimeout(() => {
      this.display = 'none';
    }, this.faidingDuration);
  }
}
