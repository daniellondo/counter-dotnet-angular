import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SideMenuComponent } from './components/side-menu/side-menu.component';
import { RouterModule } from '@angular/router';
import { MaterialModule } from '../material/material.module';
import { CounterModule } from '../counter/counter.module';

@NgModule({
  declarations: [
    SideMenuComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    MaterialModule,
    CounterModule
  ],
  exports: [
    SideMenuComponent
  ]
})
export class SharedModule { }
