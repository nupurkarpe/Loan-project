import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-emi-schedule',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    FormsModule
  ],
  templateUrl: './emi-schedule.html',
  styleUrl: './emi-schedule.css',
})
export class EmiSchedule {

}
