import { Component, ElementRef, HostBinding, OnInit } from '@angular/core';
import * as d3 from 'd3';
import * as nv from 'nvd3';
import { LineChartComponent as BaseLineChartComponent } from 'theme/components/line-chart';
import { WeightChartService } from './weight-chart.service';

@Component({
  selector: 'app-weight-chart',
  styleUrls: ['../../../../theme/components/line-chart/line-chart.component.scss'],
  template: ``,
  providers: [WeightChartService],
})
export class WeightChartComponent extends BaseLineChartComponent {
  constructor(
    el: ElementRef,
    private weightChartService: WeightChartService,
  ) {
    super(el);

    this.xAxis = 'Month';
    this.yAxis = 'Weight';
    this.maxX = 12;

    this.afterConfigure();

    this.animatedData = [
      {
        values: [],
        key: 'Yours',
        color: '#00bcd4',
      },
      {
        values: [],
        key: 'Average',
        fillOpacity: 0.00001,
        area: true,
        color: '#ffc107',
      },
    ];
    this.rawData = [
      weightChartService.getYoursGraph,
      weightChartService.getAverageGraph,
    ]
      .map(f => f.bind(weightChartService))
      .map(f => [...f(0, this.maxX + 1, this.xStep)]);
  }
}
