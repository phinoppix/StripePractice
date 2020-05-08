<script>
  import {createEventDispatcher, onMount} from 'svelte';
  import * as cache from '../cache';

  export let stripe;

  const dispatch = createEventDispatcher();
  let card;
  let formError = '';
  let inputCardholderName = 'ARIS TIRU';

  const onSubmit = ev => {
    ev.preventDefault();
    dispatch('paymentCompleted', {
      card,
      billing_details: {
        name: "Aris Tiru"
      }
    });
  };

  onMount(() => {
    const elements = stripe.elements();

    card = elements.create('card');
    card.mount('#card-element');

    card.addEventListener('change', ({error}) => {
      formError = error ? error.message : '';
    });
  })
</script>

<form id="payment-form" on:submit={onSubmit}>
  <p class="error">{formError}</p>
  <div class="card-name">
    <input class="name" placeholder="Cardholder name" bind:value={inputCardholderName} />
  </div>
  <div id="card-element">
  </div>
  <hr />
  <button id="submit">Pay Now ${cache.getIntent().amount / 100}</button>
</form>

<style>
  p.error {
    font-style: italic;
    font-size: small;
    color: red;
    margin: 12px 0;
  }
  div.card-name {
    display: flex;
    flex-direction: row;
    margin-bottom: 12px;
  }
  div.card-name > input {
    flex: 1;
    border: 1px solid transparent;
    border-bottom: 1px solid lightgray;
  }
</style>