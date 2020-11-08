import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import{ HttpClient,HttpEventType, HttpHeaders} from '@angular/common/http';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss']
})
export class UploadComponent implements OnInit {
  public progress: number;
  public message: string;
  public users;
  private headers: HttpHeaders;
  @Output() public onUploadFinished = new EventEmitter();

  constructor(private http: HttpClient,private SpinnerService: NgxSpinnerService) { 
    this.headers = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8'});
  }

  ngOnInit(): void {
  }
  
  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.SpinnerService.show();
    this.http.post('http://localhost:49776/api/upload', formData)
      .subscribe({next:data => {
        this.users = data[0].table;
        this.SpinnerService.hide();
        this.message="File uploaded successfully!"
      console.log(data);
        },error: error => {
          
          console.error('There was an error!', error);
      }
      });
  }


}
