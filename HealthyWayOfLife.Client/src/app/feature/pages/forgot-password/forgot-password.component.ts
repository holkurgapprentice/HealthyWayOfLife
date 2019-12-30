import { Component, HostBinding } from '@angular/core';

import { BlankLayoutCardComponent } from '@shared/*';

@Component({
  selector: 'app-forgot-password',
  styleUrls: ['../../../shared/blank-layout-card/blank-layout-card.component.scss'],
  templateUrl: './forgot-password.component.html',
})
export class ForgotPasswordComponent extends BlankLayoutCardComponent { }
