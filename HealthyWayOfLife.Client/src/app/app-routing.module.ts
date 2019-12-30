import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonLayoutComponent } from '@shared/*';
import { ChartsComponent } from './feature/charts';
import { ComponentsComponent } from './feature/components';
import { DashboardComponent } from './feature/dashboard';
import { FormsComponent } from './feature/forms';

@NgModule({
  imports: [
    RouterModule.forRoot(
      [
        { path: '', redirectTo: 'app/dashboard', pathMatch: 'full' },
        { path: 'app', component: CommonLayoutComponent, children: [
          { path: 'dashboard', component: DashboardComponent, pathMatch: 'full' },
          { path: 'forms', component: FormsComponent, pathMatch: 'full' },
          { path: 'charts', component: ChartsComponent, pathMatch: 'full' },
          { path: 'components', component: ComponentsComponent, pathMatch: 'full' },
          { path: '**', redirectTo: '/public/404' },
        ] }, // add 'canActivate: AuthGuard' for catching unauth users
        { path: 'ui', loadChildren: './feature/ui/ui.module#UIModule' },
        { path: 'maps', loadChildren: './feature/maps/maps.module#MapsModule' },
        { path: 'public', loadChildren: './feature/pages/pages.module#PagesModule' },
        { path: '**', redirectTo: '/public/404' },
      ],
      { useHash: true },
    ),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}