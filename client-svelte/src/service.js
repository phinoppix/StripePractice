import * as cache from './cache';
import {appEnv} from './appEnv';

const GATEWAY_SERVER_URL = appEnv.backend_url;
const GATEWAY_SERVER_URL_INIT = GATEWAY_SERVER_URL + '/api/payment/init';

const DEFAULT_PAYMENT_POST_PARAMS = {
  method: 'POST',
  mode: 'cors',
  cache: 'no-cache',
  credentials: 'same-origin',
  headers: {
    'content-type': 'application/json'
  }
};

export async function createSession(sessionInput) {
  return fetch(GATEWAY_SERVER_URL_INIT, {
    ...DEFAULT_PAYMENT_POST_PARAMS,
    body: JSON.stringify(sessionInput)
  })
    .then(response => response.json())
    .then(data => {
      console.log('createSession OK', data);
      if (data.status === 'OK') {
        cache.setIntent(JSON.parse(data.successData.paymentIntent));
        return true;
      }
      return false;
    });
}
