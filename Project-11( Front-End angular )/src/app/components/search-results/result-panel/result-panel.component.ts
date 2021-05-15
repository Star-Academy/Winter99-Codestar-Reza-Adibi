import { Component, Input, OnInit } from '@angular/core';
import { HidableFlex } from 'src/app/Interfaces/Hidables/HidableFlex';
import { TextDocument } from 'src/app/models/Document';

@Component({
  selector: 'app-result-panel',
  templateUrl: './result-panel.component.html',
  styleUrls: ['./result-panel.component.scss'],
})
export class ResultPanelComponent extends HidableFlex {
  private _document?: TextDocument;

  @Input()
  public set document(newDocument: TextDocument | undefined) {
    if (newDocument !== undefined) {
      this._document = newDocument;
      this.show();
    }
  }

  public get document(): TextDocument | undefined {
    return this._document === undefined
      ? new TextDocument('', '')
      : this._document;
  }

  constructor() {
    super();
  }
}
