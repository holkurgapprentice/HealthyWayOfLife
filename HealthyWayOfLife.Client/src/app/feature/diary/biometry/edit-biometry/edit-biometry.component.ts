import { Location } from '@angular/common';
import { Component, HostBinding, OnInit } from '@angular/core';
import { UpgradableComponent } from 'theme/components/upgradable';

@Component({
  selector: 'app-edit-biometry',
  templateUrl: './edit-biometry.component.html',
  styleUrls: ['./edit-biometry.component.scss']
})
export class EditBiometryComponent extends UpgradableComponent {
  @HostBinding('class.mdl-grid') private readonly mdlGrid = true;
  @HostBinding('class.mdl-cell') private readonly mdlCell = true;
  @HostBinding('class.mdl-cell--12-col-desktop') private readonly mdlCell12ColDesktop = true;
  @HostBinding('class.mdl-cell--12-col-tablet') private readonly mdlCell12ColTablet = true;
  @HostBinding('class.mdl-cell--4-col-phone') private readonly mdlCell4ColPhone = true;
  @HostBinding('class.mdl-cell--top') private readonly mdlCellTop = true;

  constructor(private location: Location) {
    super();
  }

  Back() {
    this.location.back();
  }
}
