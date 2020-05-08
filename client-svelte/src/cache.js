const PAYMENT_INTENT = '_stripe_payment_intent';
export const setIntent = objIntent =>
  localStorage.setItem(PAYMENT_INTENT, JSON.stringify(objIntent));

export const hasIntent = () => (localStorage.getItem(PAYMENT_INTENT) || '').length > 0;

export const getIntent = () => JSON.parse(localStorage.getItem(PAYMENT_INTENT) || '{}');

export const removeIntent = () => localStorage.removeItem(PAYMENT_INTENT);