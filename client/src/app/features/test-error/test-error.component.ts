import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-test-error',
  imports: [MatButton],
  templateUrl: './test-error.component.html',
  styleUrl: './test-error.component.css',
})
export class TestErrorComponent {
  baseUrl = "https://localhost:5001/api/"
  private http = inject(HttpClient);
  validationErrors?: string[];

  get404Error() {
    this.http.get(this.baseUrl + "buggy/notfound").subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
  get400Error() {
    this.http.get(this.baseUrl + "buggy/badrequest").subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
  get401Error() {
    this.http.get(this.baseUrl + "buggy/unauthorized").subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
  get500Error() {
    this.http.get(this.baseUrl + "buggy/internalerror").subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }
  getValidation400Error() {
    this.validationErrors = undefined;

    this.http.post(this.baseUrl + "buggy/validationerror", {}).subscribe({
      next: response => console.log(response),
      error: error => {
        if (Array.isArray(error)) {
          this.validationErrors = error;
          return;
        }

        this.validationErrors = error?.error?.errors
          ? Object.values(error.error.errors).flat() as string[]
          : ['Validation failed'];
      }
    })
  }
}
