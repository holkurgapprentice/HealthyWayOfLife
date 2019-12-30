import { Injectable } from '@angular/core';

@Injectable()
export class WeightChartService {
  private yoursFunction(x) {
    return (x * .1) * (x * .1) + (x * .1);
  }

  private averageFunction(x) {
    return 1;
  }

  public *getYoursGraph(from: any, to: number, step: any) {
    let x = from;
    do {
      yield ({ x, y: this.yoursFunction(x) });
    } while ((x += step) < to);
  }

  public *getAverageGraph(from: any, to: number, step: any) {
    let x = from;
    do {
      yield ({ x, y: this.averageFunction(x) });
    } while ((x += step) < to);
  }
}
