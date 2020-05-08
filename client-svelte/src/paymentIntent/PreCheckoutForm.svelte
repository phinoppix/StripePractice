<script>
  import {createEventDispatcher} from 'svelte';
  import * as svc from '../service';

  let inputAmount = 100.50;
  const dispatch = createEventDispatcher();

  const checkout = () => {
    dispatch('checkout', {
      amount: inputAmount,
      currency: 'usd',
      paymentMethodTypes: ['card'],
      currentPaymentIntentId: null,
      orderId: 'A192881-091'
    });
  }
</script>

<label>
  Amount:
  <input type="number" bind:value={inputAmount}/>
</label>

<button on:click={checkout}>Checkout Now</button>

<style>
  label {
    flex: 1;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    margin: 4px 0;
  }
</style>