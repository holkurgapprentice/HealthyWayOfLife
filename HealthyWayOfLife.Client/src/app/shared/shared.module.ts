import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ThemeModule } from 'theme';
import { BlankLayoutCardComponent, BlankLayoutComponent, CommonLayoutComponent, MessageMenuComponent, NotificationMenuComponent, SidebarComponent } from '.';

const components = [
  BlankLayoutComponent,
  BlankLayoutCardComponent,
  CommonLayoutComponent,
  MessageMenuComponent,
  NotificationMenuComponent,
  SidebarComponent,
];

@NgModule({
  imports: [
    CommonModule,
    ThemeModule,
    RouterModule,
  ],
  declarations: [
    components,
  ],
  exports: [
    components,
  ],
})
export class SharedModule { }
