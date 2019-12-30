import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SharedModule } from 'app/shared/shared.module';
import { ThemeModule } from 'theme';
import { CotoneasterCardComponent } from './cotoneaster-card';
import { DashboardComponent } from './dashboard.component';
import { PieChartComponent } from './pie-chart';
import { RobotCardComponent } from './robot-card';
import { TableCardComponent } from './table-card';
import { TodoListComponent } from './todo-list';
import { TrendingComponent } from './trending';
import { WeatherComponent } from './weather';
import { WeightChartComponent } from './weight-chart';

@NgModule({
  imports: [
    CommonModule,
    ThemeModule,
    FormsModule,
    SharedModule,
  ],
  declarations: [
    DashboardComponent,
    WeightChartComponent,
    PieChartComponent,
    WeatherComponent,
    CotoneasterCardComponent,
    TableCardComponent,
    RobotCardComponent,
    TodoListComponent,
    TrendingComponent,
  ],
  exports: [
    WeatherComponent,
    TrendingComponent,
  ],
})
export class DashboardModule { }
