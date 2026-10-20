import { Component, input } from '@angular/core';
import { CartItem } from '../../../shared/models/cart';
import { RouterLink } from '@angular/router';
import { MatIcon } from '@angular/material/icon';
import { CurrencyPipe } from '@angular/common';
import { MatAnchor } from '@angular/material/button';

@Component({
  selector: 'app-cart-item',
  imports: [RouterLink, MatIcon, CurrencyPipe, MatAnchor],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.css',
})
export class CartItemComponent {
  item = input.required<CartItem>();
}
