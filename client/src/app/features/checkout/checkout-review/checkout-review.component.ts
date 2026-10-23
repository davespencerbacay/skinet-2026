import { CurrencyPipe } from '@angular/common';
import { Component, inject, Input } from '@angular/core';
import { CartService } from '../../../core/services/cart.service';
import { ConfirmationToken } from '@stripe/stripe-js';
import { AddressPipe } from '../../../shared/pipes/address-pipe';

@Component({
  selector: 'app-checkout-review',
  imports: [CurrencyPipe, AddressPipe],
  templateUrl: './checkout-review.component.html',
  styleUrl: './checkout-review.component.css',
})
export class CheckoutReviewComponent {
  cartService = inject(CartService);

  @Input() confirmationToken?: ConfirmationToken;
}
