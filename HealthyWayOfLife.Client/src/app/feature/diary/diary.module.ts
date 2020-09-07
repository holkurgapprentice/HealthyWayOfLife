import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MaterialAngularSelectModule } from 'material-angular-select';
import { ThemeModule } from 'theme';
import { BiometryComponent } from './biometry/biometry.component';
import { DiaryRoutingModule } from './diary-routing.module';
import { NotesComponent } from './notes/notes.component';
import { EditBiometryComponent } from './biometry/edit-biometry/edit-biometry.component';

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
    EditBiometryComponent,
  ],
  providers: [
  ],
})
export class DiaryModule { }
