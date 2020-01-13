import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MaterialAngularSelectModule } from 'material-angular-select';
import { ThemeModule } from 'theme';
import { BiometryComponent } from './biometry/biometry.component';
import { DiaryRoutingModule } from './diary-routing.module';
import { NotesComponent } from './notes/notes.component';

@NgModule({
  imports: [
    CommonModule,
    DiaryRoutingModule,
    ThemeModule,
    MaterialAngularSelectModule,
  ],
  declarations: [
    BiometryComponent,
    NotesComponent,
  ],
  providers: [
  ],
})
export class DiaryModule { }
