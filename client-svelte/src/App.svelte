<script>
	import PaymentController from './paymentIntent/PaymentController.svelte';
</script>

<div>
	<h1>Stripe Client</h1>
	<h2>Payment Intent</h2>
	<main>
		<PaymentController />
	</main>
</div>

<style>
	h1 {
		color: #ff3e00;
		text-transform: uppercase;
		font-size: 2em;
		font-weight: 100;
	}

	div {
		display: flex;
		flex-direction: column;
		align-items: center;
	}

	@media (min-width: 640px) {
		main {
			max-width: none;
		}
	}

	main {
		width: 100%;
		max-width: 400px;
		display: flex;
		flex-direction: column;
		justify-content: center;
	}
</style>