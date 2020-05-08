<script>
  export let errorType; // error | info | <nil>
  export let errorMessage; // string or Error

  let error, info;
  $: info = errorType === 'error' ? undefined : errorMessage;
  $: error = errorType === 'info' ? undefined : errorMessage;
</script>

{#if (error || info)}
  <p class:error={!!error} class:info={!!info} role="alert">* {error || info}</p>
{/if}

<style>
  p.error {
    color: red;
  }
  p.info {
    color: green;
  }
</style>