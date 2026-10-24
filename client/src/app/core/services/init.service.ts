import { inject, Injectable } from '@angular/core';
import { CartService } from './cart.service';
import { catchError, forkJoin, of, switchMap, tap } from 'rxjs';
import { AccountService } from './account.service';
import { SignalrService } from './signalr.service';

@Injectable({
  providedIn: 'root',
})
export class InitService {
  private cartService = inject(CartService);
  private accountService = inject(AccountService);
  private signalRService = inject(SignalrService);

  init() {
    const cartId = localStorage.getItem("cart_id");
    const cart$ = cartId ? this.cartService.getCart(cartId) : of(null)
    const user$ = this.accountService.getAuthState().pipe(
      switchMap(state => state.isAuthenticated ? this.accountService.getUserInfo() : of(null)),
      catchError(() => of(null))
    );

    return forkJoin({
      cart: cart$,
      user: user$.pipe(
        tap((user) => {
          if (user) {
            this.signalRService.createHubConnection();
          }
        })
      )
    });
  }
}
