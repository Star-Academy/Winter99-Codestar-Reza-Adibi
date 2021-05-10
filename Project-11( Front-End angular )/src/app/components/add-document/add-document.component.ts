import { Component, OnInit } from '@angular/core';
import { AddDocumentService } from 'src/app/services/add-document/add-document.service';

@Component({
  selector: 'app-add-document',
  templateUrl: './add-document.component.html',
  styleUrls: ['./add-document.component.scss'],
})
export class AddDocumentComponent implements OnInit {
  constructor(private addService: AddDocumentService) {}

  ngOnInit(): void {}

  sendAddRequest() {}
}
