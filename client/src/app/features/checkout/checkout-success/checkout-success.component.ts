import { Component, inject, OnDestroy } from '@angular/core';
import { SignalrService } from '../../../core/services/signalr.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AddressPipe } from '../../../shared/pipes/address-pipe';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { PaymentCardPipe } from '../../../shared/pipes/payment-card-pipe';
import { OrderService } from '../../../core/services/order.service';

@Component({
  selector: 'app-checkout-success',
  imports: [MatProgressSpinnerModule, AddressPipe, CurrencyPipe, PaymentCardPipe, DatePipe],
  templateUrl: './checkout-success.component.html',
  styleUrl: './checkout-success.component.css',
})
export class CheckoutSuccessComponent implements OnDestroy {
  signalRService = inject(SignalrService);
  private orderService = inject(OrderService);

  ngOnDestroy(): void {
    this.orderService.orderComplete = false;
    this.signalRService.orderSignal.set(null);
  }

}
