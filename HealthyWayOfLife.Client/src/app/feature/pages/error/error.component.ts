import { Component, HostBinding } from '@angular/core';

import { BlankLayoutCardComponent } from '@shared/*';

@Component({
  selector: 'app-error',
  styleUrls: ['../../../shared/blank-layout-card/blank-layout-card.component.scss'],
  templateUrl: './error.component.html',
})
export class ErrorComponent extends BlankLayoutCardComponent { }
