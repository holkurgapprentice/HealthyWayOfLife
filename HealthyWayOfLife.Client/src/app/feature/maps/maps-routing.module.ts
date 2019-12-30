import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonLayoutComponent } from 'app/shared/common-layout';
import { SharedModule } from 'app/shared/shared.module';
import { MapComponent } from './map';
import { MapAdvancedComponent } from './map-advanced';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: '',
        component: CommonLayoutComponent,
        children: [
          { path: 'simple', component: MapComponent, pathMatch: 'full' },
          { path: 'advanced', component: MapAdvancedComponent, pathMatch: 'full' },
        ],
      },
    ]),
    SharedModule,
  ],
  exports: [RouterModule],
})
export class MapsRoutingModule {}
