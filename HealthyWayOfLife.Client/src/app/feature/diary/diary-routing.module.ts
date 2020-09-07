import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonLayoutComponent } from 'app/shared/common-layout';
import { SharedModule } from 'app/shared/shared.module';
import { BiometryComponent } from './biometry/biometry.component';
import { EditBiometryComponent } from './biometry/edit-biometry/edit-biometry.component';
import { NotesComponent } from './notes/notes.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: '',
        component: CommonLayoutComponent,
        children: [
          { path: 'biometry/:id', component: EditBiometryComponent, pathMatch: 'full' },
          { path: 'biometry', component: BiometryComponent, pathMatch: 'full' },
          { path: 'notes', component: NotesComponent, pathMatch: 'full' },
        ],
      },
    ]),
    SharedModule,
  ],
  exports: [RouterModule],
})
export class DiaryRoutingModule { }
