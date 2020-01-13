import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MglTimelineModule } from 'angular-mgl-timeline';
import { ThemeModule } from 'theme';
import { BadgesComponent } from './badges';
import { ChipsComponent } from './chips';
import { ComponentsComponent } from './components.component';
import { ProgressBarsComponent } from './progress-bars';
import { SlidersComponent } from './sliders';
import { TogglesComponent } from './toggles';
import { TooltipsComponent } from './tooltips';

@NgModule({
  imports: [
    CommonModule,
    ThemeModule,
    FormsModule,
    MglTimelineModule,
  ],
  exports: [
    ComponentsComponent,
    MglTimelineModule,
  ],
  declarations: [
    ComponentsComponent,
    TogglesComponent,
    ProgressBarsComponent,
    ChipsComponent,
    SlidersComponent,
    TooltipsComponent,
    BadgesComponent,
  ],
})
export class ComponentsModule { }
