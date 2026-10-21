import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { MatFormField, MatLabel } from '@angular/material/select';
import { MatInput } from '@angular/material/input';
import { MatIcon } from '@angular/material/icon';
import { CartService } from '../../../core/services/cart.service';
import { CurrencyPipe, Location } from '@angular/common';

@Component({
  selector: 'app-order-summary',
  imports: [MatButton, RouterLink, MatFormField, MatLabel, MatInput, MatIcon, CurrencyPipe],
  templateUrl: './order-summary.component.html',
  styleUrl: './order-summary.component.css',
})
export class OrderSummaryComponent {
  cartService = inject(CartService);
  location = inject(Location);
}
