import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-closure',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    FormsModule
  ],
  templateUrl: './closure.html',
  styleUrl: './closure.css',
})
export class Closure {

}
