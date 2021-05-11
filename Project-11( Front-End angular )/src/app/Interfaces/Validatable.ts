import { HttpErrorResponse } from '@angular/common/http';

export abstract class Validatable {
  public errorValue: HttpErrorResponse | undefined;

  protected validateResponse(error: HttpErrorResponse) {
    this.errorValue = error;
  }

  public removeError() {
    this.errorValue = undefined;
  }
}
