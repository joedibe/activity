import { Component, OnInit, ViewChild } from '@angular/core';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { MatPaginator, MatSort, MatTableDataSource, MatDialog } from '@angular/material';
import { DocumentService } from 'src/services/document.service';
import { Document } from 'src/domain/document';

@Component({
  selector: 'app-document',
  templateUrl: './document.component.html',
  styleUrls: ['./document.component.css'],
  providers: [DocumentService, DatePipe]
})

export class DocumentComponent implements OnInit {

  dataSource;
  displayedColumns: string[] = ['demo', 'documentName', 'price', 'isActive', 'dateCreated'];
  documentList: Document[];
  selectDocument: Document;
  documentFormGroup: FormGroup;
  isAddDocument: boolean;
  indexOfDocument: number = 0;
  isDeleteDocument: boolean;
  totalRecords: number = 0;
  dateCreated: Date;
  isActivated: string[] = ['True', 'False'];
  minDate = new Date(Date.now());

  constructor(private documentService: DocumentService, private fb: FormBuilder, private datePipe: DatePipe) { }

  @ViewChild(MatPaginator) paginator: MatPaginator;
  
  ngOnInit() {
    this.documentFormGroup = this.fb.group({
      documentName: ['', Validators.required],
      price: ['', Validators.required],
      isActive: ['', Validators.required],
      dateCreated: ['', Validators.required]
    });

    this.loadAllDocuments();
  }

  loadAllDocuments() {
    
    this.documentService.getDocument().then(documents => {
      
      this.documentList = documents;

      for (let i = 0; i < this.documentList.length; i++) {
        this.documentList[i].dateCreated =  this.datePipe.transform(this.documentList[i].dateCreated, 'yyyy-MM-dd');
      }

      this.dataSource = new MatTableDataSource<Document>(this.documentList);
      this.dataSource.paginator = this.paginator;
    
    });

  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  

  addDocument() {
    this.documentFormGroup.enable();
    this.isDeleteDocument = false;
    this.isAddDocument = true;
    this.selectDocument = {} as Document;
    this.selectDocument.isActive = true; 
    this.loadAllDocuments();
  }
  
  
  editDocument(Document) {
    this.documentFormGroup.enable();
    this.isDeleteDocument = false;
    this.indexOfDocument = this.documentList.indexOf(Document);
    this.isAddDocument = false;
    this.selectDocument = Document;
    this.selectDocument.isActive = this.documentList[this.indexOfDocument].isActive;
    this.selectDocument = Object.assign({}, this.selectDocument);
  
    this.dateCreated = new Date(this.selectDocument.dateCreated);
    this.loadAllDocuments();
  }
  
  deleteDocument(Document) {
    this.documentFormGroup.disable();
    this.isDeleteDocument = true;
    this.indexOfDocument = this.documentList.indexOf(Document);
    this.selectDocument = Document;
    this.selectDocument = Object.assign({}, this.selectDocument);
    this.loadAllDocuments();
  }
  
  okDelete() {
    let tmpDocumentList = [...this.documentList];
    this.documentService.deleteDocument(this.selectDocument.documentID)
      .then(() => {
        tmpDocumentList.splice(this.indexOfDocument, 1);
        this.documentList = tmpDocumentList;
        this.selectDocument = null;
        this.loadAllDocuments();
      });
  }
  
  
  saveDocument() {
    let tmpDocumentList = [...this.documentList];
  
    this.selectDocument.dateCreated =
      this.datePipe.transform(this.selectDocument.dateCreated, 'yyyy-MM-dd');
    if (this.isAddDocument == true) {
      this.documentService.postDocument(this.selectDocument).then(result => {
        result.dateCreated =
          this.datePipe.transform(this.selectDocument.dateCreated, 'yyyy-MM-dd');
        tmpDocumentList.push(result);
        this.documentList = tmpDocumentList;
        this.selectDocument = null;
        this.loadAllDocuments();
      });
    }
    else {
      this.documentService.putDocument(this.selectDocument.documentID, this.selectDocument).then(result => {
        result.dateCreated =
          this.datePipe.transform(this.selectDocument.dateCreated, 'yyyy-MM-dd');
        tmpDocumentList[this.indexOfDocument] = result;
        this.documentList = tmpDocumentList;
        this.selectDocument = null;
        this.loadAllDocuments();
      });
    }
  
  }
  
  cancelDocument() {
    this.selectDocument = null;
  }
}

