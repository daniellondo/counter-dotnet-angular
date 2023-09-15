import { Component, OnInit } from '@angular/core';
import { CounterService } from '../../services/counter.service';
import { Counter } from '../../interfaces/counter.interface';
import { delay } from 'rxjs';

@Component({
  selector: 'counter-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css'],
})
export class FormComponent implements OnInit {
  public counter : Counter = {
    count : 0,
  };

  constructor(private service : CounterService) {}

  ngOnInit(): void {
    this.service.getCounter$().subscribe(counter => {
      this.counter = counter;
    });
  }

  sumCounter(){
    let payload = this.counter;
    payload.count = payload.count + 1;
    this.service.updateCounter(payload).then();
  }

  resetCounter(){
    let payload = this.counter;
    payload.count = 0;
    this.service.updateCounter(payload).then();
  }

}
