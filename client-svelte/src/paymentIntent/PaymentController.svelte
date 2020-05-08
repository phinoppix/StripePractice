<script>
  import {PAYMENT_FLOW} from '../utils';
  import PreCheckoutForm from './PreCheckoutForm.svelte';
  import CheckoutForm from './CheckoutForm.svelte';
  import PaymentConfirmation from '../common/PaymentConfirmation.svelte';
  import * as svc from '../service';
  import * as cache from '../cache';
  import InlineAlert from '../common/InlineAlert.svelte';

  const stripe = new Stripe('');

  let nextStep = PAYMENT_FLOW.PRE_CHECKOUT;
  let errorType = '', errorMessage = '';


  const checkout = e => {
    errorType = undefined;
    try {
      svc.createSession(e.detail)
          .then(data => {
            nextStep = PAYMENT_FLOW.CHECKING_OUT;
          })
          .catch(e => {
            errorType = 'error';
            errorMessage = e;
            console.log('Error caught', e);
          });
    } catch (e) {
      errorType = 'error';
      errorMessage = e;
      console.log('Error caught', e);
    }
  };


  const paymentCompleted = e => {
    stripe.confirmCardPayment(
        cache.getIntent().client_secret, {
          payment_method: {
            ...e.detail
          }
        }
    ).then(status => {
      if (status.error) {
        console.log('paymentCompleted() error', {
          error: status.error,
          detail: e.detail
        });
        errorType = 'error';
        errorMessage = status.error.message;
        // TODO: enable to payment button
      } else {
        console.log('payment processed', status);
        cache.removeIntent();
        nextStep = PAYMENT_FLOW.COMPLETED;
      }
    });
  };
</script>

<InlineAlert {errorType} {errorMessage}/>
<!--  Ideally, view switching/rendering are managed by a router -->
{#if nextStep === PAYMENT_FLOW.PRE_CHECKOUT}
  <PreCheckoutForm on:checkout={checkout}/>
{:else if nextStep === PAYMENT_FLOW.CHECKING_OUT}
  <CheckoutForm
      on:paymentCompleted={paymentCompleted}
      {stripe}/>
{:else}
  <PaymentConfirmation/>
{/if}
