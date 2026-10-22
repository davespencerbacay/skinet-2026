import { loadStripe, Stripe, StripeAddressElement, StripeAddressElementOptions, StripeElements, StripePaymentElement } from '@stripe/stripe-js';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CartService } from './cart.service';
import { Cart } from '../../shared/models/cart';
import { firstValueFrom, map } from 'rxjs';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root',
})
export class StripService {
  baseUrl = environment.apiUrl;
  private cartService = inject(CartService);
  private accountService = inject(AccountService)
  private http = inject(HttpClient);
  private stripePromise: Promise<Stripe | null>;
  private elements?: StripeElements
  private addressElements?: StripeAddressElement;
  private paymentElement?: StripePaymentElement;

  constructor() {
    this.stripePromise = loadStripe(environment.stripePublicKey);
  }

  getStripeInstance() {
    return this.stripePromise;
  }

  async createPaymentElement() {
    if (!this.paymentElement) {
      const elements = await this.initializeElements();

      if (elements) {
        this.paymentElement = elements.create("payment");
      } else {
        throw new Error("Elements instance has not been initialized.")
      }
    }

    return this.paymentElement;
  }

  async createAddressElement() {
    if (!this.addressElements) {
      const elements = await this.initializeElements();
      if (elements) {
        const user = this.accountService.currentUser();
        let defaultValues: StripeAddressElementOptions["defaultValues"] = {};

        if (user) {
          defaultValues.name = user.firstName + " " + user.lastName
        }

        if (user?.address) {
          defaultValues.address = {
            line1: user.address.line1,
            line2: user.address.line2,
            city: user.address.city,
            postal_code: user.address.postalCode,
            state: user.address.state,
            country: user.address.country,
          }
        }
        const options: StripeAddressElementOptions = {
          mode: "shipping",
          defaultValues
        }
        this.addressElements = elements.create("address", options);
      } else {
        throw new Error("Elements instance has not been loaded.")
      }
    }

    return this.addressElements;
  }

  async initializeElements() {
    if (!this.elements) {
      const stripe = await this.getStripeInstance();
      if (stripe) {
        const cart = await firstValueFrom(this.createOrUpdatePaymentIntent());
        this.elements = stripe.elements({ clientSecret: cart.clientSecret, appearance: { labels: "floating" } })
      } else {
        throw new Error("Strip has not been lo9aded.")
      }
    }

    return this.elements;
  }

  createOrUpdatePaymentIntent() {
    const cart = this.cartService.cart();

    if (!cart) throw new Error("Problem with cart");

    return this.http.post<Cart>(this.baseUrl + "payments/" + cart.id, {}).pipe(
      map(cart => {
        this.cartService.setCart(cart);
        return cart;
      })
    );
  }

  disposeElements() {
    this.elements = undefined;
    this.addressElements = undefined;
    this.paymentElement = undefined;
  }
}
