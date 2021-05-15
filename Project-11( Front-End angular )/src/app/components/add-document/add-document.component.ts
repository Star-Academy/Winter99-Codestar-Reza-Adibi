import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import { Validatable } from 'src/app/Interfaces/Validatable';
import { TextDocument } from 'src/app/models/Document';
import { AddDocumentService } from 'src/app/services/add-document/add-document.service';

@Component({
  selector: 'app-add-document',
  templateUrl: './add-document.component.html',
  styleUrls: ['./add-document.component.scss'],
})
export class AddDocumentComponent extends Validatable {
  id = new FormControl('', [Validators.required]);
  content = new FormControl('', [Validators.required]);

  constructor(private addService: AddDocumentService) {
    super();
  }

  sendAddRequest(): void {
    this.validateForm();
    this.addService
      .Add(new TextDocument(this.id.value, this.content.value))
      .subscribe(
        (success) => {
          alert('Success!');
        },
        (error) => {
          this.validateResponse(error);
        }
      );
  }

  private validateForm() {
    if (this.id.invalid) {
      alert('Id is required!');
      throw new Error('Id is required!');
    }
    if (this.content.invalid) {
      alert('Content is required!');
      throw new Error('Content is required!');
    }
  }
}
