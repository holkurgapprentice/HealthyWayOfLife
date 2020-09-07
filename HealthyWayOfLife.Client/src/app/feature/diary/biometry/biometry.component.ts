import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-biometry',
  templateUrl: './biometry.component.html',
  styleUrls: ['./biometry.component.scss']
})
export class BiometryComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }

  AddNew() {
    this.router.navigateByUrl(`/diary/biometry/0`);
  }

}
