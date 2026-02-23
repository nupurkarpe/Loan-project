import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-sanction-disbursement',
  imports: [
    MatCardModule,
    MatButtonModule,
    MatInputModule,
    FormsModule,
    RouterLink
  ],
  templateUrl: './sanction-disbursement.html',
  styleUrl: './sanction-disbursement.css',
})
export class SanctionDisbursement {

}
