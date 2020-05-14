# Stripe Practice

### Requirements

To run, setup the Stripe API and Webhook keys using .NET `Secrets Manager`:
```
dotnet user-secrets set "stripe:api_key" {{api key}}
dotnet user-secrets set "stripe:webhook" {{webhook key}}
```

To run the webhooks locally, use Stripe CLI:
```
# Install the CLI
$ brew install stripe/stripe-cli/stripe

# Login the CLI using the API key, a pairing code will be generated.
$ stripe login --api-key {{api key}}

# Start listening (assuming backend server runs on port 5001)...
$ stripe listen --forward-to https://localhost:5001/api/payment/webhook
```