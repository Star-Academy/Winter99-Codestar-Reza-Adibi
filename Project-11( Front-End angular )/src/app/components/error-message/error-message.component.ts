import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input } from '@angular/core';

import { HidableBlock } from 'src/app/Interfaces/Hidables/HidableBlock';

@Component({
  selector: 'app-error-message',
  templateUrl: './error-message.component.html',
  styleUrls: ['./error-message.component.scss'],
})
export class ErrorMessageComponent extends HidableBlock {
  private _error: HttpErrorResponse | undefined;

  @Input()
  public set error(value: HttpErrorResponse | undefined) {
    if (value !== undefined) {
      this._error = value;
      this.show();
    }
  }

  public get errorMessage(): string {
    if (this._error === undefined) {
      return '';
    }
    let message: string;
    switch (this._error.status / 100) {
      case 4:
        message = 'Connection Error';
        break;
      case 5:
        message = 'Server Error';
        break;
      default:
        message = 'Unknown Error';
        break;
    }
    return `Error message: ${message}.`;
  }

  constructor() {
    super();
  }
}
